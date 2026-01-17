// src/hooks.server.ts
import type { HandleServerError } from '@sveltejs/kit';

export const handleError = (({ error, event }) => {
	const message = error instanceof Error ? error.message : 'Unknown';
	return {
		message: message,
	};
}) satisfies HandleServerError;
