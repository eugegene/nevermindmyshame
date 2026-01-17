import { error } from '@sveltejs/kit';
import { PUBLIC_API_URL } from '$env/static/public';
import { goto } from '$app/navigation';
import { browser } from '$app/environment';
import { storage } from '$lib/utils/storage';
import { resolve } from '$app/paths';

export interface SendOptions {
	method: 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
	path: string;
	data?: unknown;
	token?: string;
	customFetch?: typeof fetch;
}

const BASE_URL = PUBLIC_API_URL.replace(/\/$/, '');

async function send<T>({ method, path, data, token, customFetch }: SendOptions): Promise<T> {
	const opts: RequestInit = { method };
	opts.headers = {
		'Content-Type': 'application/json',
	};

	const authToken = token || storage.getToken();

	if (authToken) {
		opts.headers['Authorization'] = `Bearer ${authToken}`;
	}

	if (data) {
		opts.body = JSON.stringify(data);
	}

	const fetchImpl = customFetch || fetch;
	const url = `${BASE_URL}/${path}`;

	try {
		const res = await fetchImpl(url, opts);

		if (res.status === 401) {
			if (browser) {
				storage.removeToken();
				if (window.location.pathname !== '/auth/login') {
					goto(resolve('/auth/login'));
				}
			}
			throw error(401, 'Unauthorized');
		}

		if (!res.ok) {
			let errMessage = 'Помилка сервера';

			// ВИПРАВЛЕННЯ ТУТ:
			// 1. Читаємо тіло як текст (один раз)
			const errorText = await res.text();

			try {
				// 2. Пробуємо розпарсити цей текст як JSON
				const errData = JSON.parse(errorText);
				errMessage = errData.error || errData.message || errMessage;
			} catch {
				// 3. Якщо парсинг не вдався, використовуємо сирий текст
				errMessage = errorText || errMessage;
			}

			throw error(res.status, errMessage);
		}

		if (res.status === 204) return {} as T;

		const responseData = await res.json();

		if (responseData && typeof responseData === 'object' && 'data' in responseData) {
			return responseData.data as T;
		}

		return responseData as T;
	} catch (e) {
		console.error(`API Error [${method} ${path}]:`, e);
		throw e;
	}
}

export const api = {
	get: <T>(path: string, token?: string, cf?: typeof fetch) =>
		send<T>({ method: 'GET', path, token, customFetch: cf }),
	post: <T>(path: string, data: unknown, token?: string, cf?: typeof fetch) =>
		send<T>({ method: 'POST', path, data, token, customFetch: cf }),
	put: <T>(path: string, data: unknown, token?: string, cf?: typeof fetch) =>
		send<T>({ method: 'PUT', path, data, token, customFetch: cf }),
	delete: <T>(path: string, token?: string, cf?: typeof fetch) =>
		send<T>({ method: 'DELETE', path, token, customFetch: cf }),
};
