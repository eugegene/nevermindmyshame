// src/hooks.server.ts
import { env } from '$env/dynamic/private';
// import type { HandleServerError } from '@sveltejs/kit';

// export const handleError = (({ error, event }) => {
// 	const message = error instanceof Error ? error.message : 'Unknown';
// 	return {
// 		message: message,
// 	};
// }) satisfies HandleServerError;

const SECRET_KEY = env.JWT_PRIVATE_KEY;

export const handle: Handle = async ({ event, resolve }) => {
	const accessToken = event.cookies.get('accessToken');
	const refreshToken = event.cookies.get('refreshToken');

	let user = null;

	if (accessToken) {
		try {
			// Перевіряємо підпис та термін дії
			const decoded = jwt.verify(accessToken, SECRET_KEY) as jwt.JwtPayload;

			// Мапимо дані з токена у структуру користувача
			user = {
				email:
					decoded.email ||
					decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
				role:
					decoded.role ||
					decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
					'User',
				username: decoded.unique_name || decoded.nameid || 'User',
				id: decoded.sub,
			};
		} catch (err) {
			// Токен невалідний або прострочений
			console.log('Token verification failed:', (err as Error).message);
		}
	}

	// Якщо токен невалідний, але є refresh token - пробуємо оновити
	if (!user && refreshToken) {
		try {
			// Викликаємо API для оновлення токена
			// Важливо: customFetch: event.fetch дозволяє робити запити всередині серверного хука
			const response = await AuthService.renewToken(refreshToken, event.fetch);

			if (response && response.accessToken) {
				const newAccessToken = response.accessToken;
				const newRefreshToken = response.refreshToken; // Якщо бекенд повертає новий refresh token

				// Верифікуємо новий токен
				const decoded = jwt.verify(newAccessToken, SECRET_KEY) as jwt.JwtPayload;

				user = {
					email:
						decoded.email ||
						decoded[
							'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
						],
					role:
						decoded.role ||
						decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
						'User',
					username: decoded.unique_name || decoded.nameid || 'User',
					id: decoded.sub,
				};

				// Оновлюємо куки
				const cookieOpts = {
					path: '/',
					httpOnly: true,
					secure: process.env.NODE_ENV === 'production',
					maxAge: 60 * 60 * 24 * 7, // 7 днів
				};

				// Access token живе менше, наприклад 1 годину, але тут для куки ставимо час життя сесії або більше
				event.cookies.set('accessToken', newAccessToken, { ...cookieOpts, maxAge: 3600 });
				if (newRefreshToken) {
					event.cookies.set('refreshToken', newRefreshToken, cookieOpts);
				}
			}
		} catch (error) {
			console.error('Token renewal failed in hooks:', error);
			// Якщо оновлення не вдалося - очищаємо куки, щоб користувач залогінився знову
			event.cookies.delete('accessToken', { path: '/' });
			event.cookies.delete('refreshToken', { path: '/' });
		}
	}
};
