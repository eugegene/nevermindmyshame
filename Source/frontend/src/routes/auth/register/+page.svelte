<script lang="ts">
	import { goto } from '$app/navigation';
	import { AuthService } from '$lib/services/auth.service';
	import { storage } from '$lib/utils/storage';
	import {
		Validator,
		RequiredStrategy,
		EmailStrategy,
		MinLengthStrategy,
		PasswordMatchStrategy,
	} from '$lib/utils/validation';
	import { resolve } from '$app/paths';

	let username = $state('');
	let email = $state('');
	let password = $state('');
	let confirmPassword = $state('');

	let isLoading = $state(false);
	let serverError = $state('');

	let errors = $state({ username: '', email: '', password: '', confirm: '' });

	function validateForm(): boolean {
		errors.username =
			Validator.validate(username, [RequiredStrategy, MinLengthStrategy(3)]) || '';
		errors.email = Validator.validate(email, [RequiredStrategy, EmailStrategy]) || '';
		errors.password =
			Validator.validate(password, [RequiredStrategy, MinLengthStrategy(6)]) || '';
		errors.confirm =
			Validator.validate(
				confirmPassword,
				[RequiredStrategy, PasswordMatchStrategy],
				password,
			) || '';

		return !errors.username && !errors.email && !errors.password && !errors.confirm;
	}

	async function handleRegister(e: Event) {
		e.preventDefault();
		serverError = '';

		if (!validateForm()) return;

		isLoading = true;

		try {
			const user = await AuthService.register({ username, email, password });

			if (user && user.accessToken) {
				storage.setToken(user.accessToken);
				if (user.refreshToken) localStorage.setItem('refresh_token', user.refreshToken);
				goto(resolve('/profile'));
			} else {
				await goto(resolve('/auth/login'));
			}
		} catch (err: any) {
			console.error(err);
			serverError =
				err.body?.message ||
				(typeof err === 'string'
					? err
					: 'Помилка реєстрації. Можливо, такий нікнейм або email вже зайняті.');
		} finally {
			isLoading = false;
		}
	}
</script>

<div class="flex items-center justify-center min-h-[80vh]">
	<div class="w-full max-w-md bg-bkg-header p-8 rounded-xl shadow-2xl border border-gray-800">
		<h1 class="text-3xl font-black text-white/95 mb-6 text-center tracking-wide">
			Створити акаунт
		</h1>

		{#if serverError}
			<div
				class="mb-4 p-3 rounded bg-red-500/10 border border-red-500/50 text-red-200 text-sm text-center animate-pulse"
			>
				{serverError}
			</div>
		{/if}

		<form onsubmit={handleRegister} class="space-y-5" novalidate>
			<div>
				<label for="username" class="block text-sm font-medium text-text-muted mb-1"
					>Нікнейм</label
				>
				<input
					id="username"
					type="text"
					bind:value={username}
					onblur={() =>
						(errors.username =
							Validator.validate(username, [
								RequiredStrategy,
								MinLengthStrategy(3),
							]) || '')}
					required
					class="w-full bg-bkg-main text-white/95 px-4 py-3 rounded-lg border {errors.username
						? 'border-red-500'
						: 'border-gray-700'} focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
				{#if errors.username}
					<p class="text-red-400 text-xs mt-1">{errors.username}</p>
				{/if}
			</div>

			<div>
				<label for="email" class="block text-sm font-medium text-text-muted mb-1"
					>Email</label
				>
				<input
					id="email"
					type="email"
					bind:value={email}
					onblur={() =>
						(errors.email =
							Validator.validate(email, [RequiredStrategy, EmailStrategy]) || '')}
					required
					class="w-full bg-bkg-main text-white/95 px-4 py-3 rounded-lg border {errors.email
						? 'border-red-500'
						: 'border-gray-700'} focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
				{#if errors.email}
					<p class="text-red-400 text-xs mt-1">{errors.email}</p>
				{/if}
			</div>

			<div>
				<label for="password" class="block text-sm font-medium text-text-muted mb-1"
					>Пароль</label
				>
				<input
					id="password"
					type="password"
					bind:value={password}
					onblur={() =>
						(errors.password =
							Validator.validate(password, [
								RequiredStrategy,
								MinLengthStrategy(6),
							]) || '')}
					required
					class="w-full bg-bkg-main text-white/95 px-4 py-3 rounded-lg border {errors.password
						? 'border-red-500'
						: 'border-gray-700'} focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
				{#if errors.password}
					<p class="text-red-400 text-xs mt-1">{errors.password}</p>
				{/if}
			</div>

			<div>
				<label for="confirm_password" class="block text-sm font-medium text-text-muted mb-1"
					>Підтвердіть пароль</label
				>
				<input
					id="confirm_password"
					type="password"
					bind:value={confirmPassword}
					oninput={() =>
						(errors.confirm =
							Validator.validate(
								confirmPassword,
								[PasswordMatchStrategy],
								password,
							) || '')}
					required
					class="w-full bg-bkg-main text-white/95 px-4 py-3 rounded-lg border {errors.confirm
						? 'border-red-500'
						: 'border-gray-700'} focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
				{#if errors.confirm}
					<p class="text-red-400 text-xs mt-1">{errors.confirm}</p>
				{/if}
			</div>

			<button
				type="submit"
				disabled={isLoading}
				class="w-full bg-white hover:bg-gray-200 text-black font-bold py-3 rounded-lg transition-all transform active:scale-95 disabled:opacity-50 disabled:cursor-not-allowed"
			>
				{#if isLoading}
					<span
						class="inline-block w-4 h-4 border-2 border-black border-t-transparent rounded-full animate-spin mr-2"
					></span>
					Реєстрація...
				{:else}
					Створити акаунт
				{/if}
			</button>
		</form>

		<div class="mt-6 text-center text-sm text-text-muted">
			Вже є акаунт?
			<a
				href={resolve('/auth/login')}
				class="text-white/95 hover:text-brand-accent font-semibold transition-colors"
			>
				Увійти
			</a>
		</div>
	</div>
</div>
