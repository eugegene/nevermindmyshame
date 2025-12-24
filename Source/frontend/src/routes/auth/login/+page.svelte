<script lang="ts">
	import { AuthService } from '$lib/services/auth.service';
	import { storage } from '$lib/utils/storage';
	import { goto } from '$app/navigation';

	let email = $state('');
	let password = $state('');
	let isLoading = $state(false);
	let errorMsg = $state('');

	async function handleLogin(e: Event) {
		e.preventDefault();
		isLoading = true;
		errorMsg = '';

		try {
			const user = await AuthService.login({ email, password });
			
			if (user.accessToken) {
				storage.setToken(user.accessToken);
			}

			goto('/profile');
		} catch (err: any) {
			console.error(err);
			errorMsg = err.body?.message || "Невірний email або пароль.";
		} finally {
			isLoading = false;
		}
	}

    // // --- ТИМЧАСОВА ІМІТАЦІЯ ---
    // async function handleLogin(e: Event) {
    //     e.preventDefault();
    //     isLoading = true;
    //     errorMsg = '';

    //     try {
    //         console.log("Імітація запиту до бекенду...");
            
    //         const fakeUser = {
    //             email: email,
    //             accessToken: "fake_jwt_token_1234567890", // Фейковий токен
    //             refreshToken: "fake_refresh_token",
    //             expiresIn: 3600
    //         };

    //         await new Promise(r => setTimeout(r, 500));

    //         storage.setToken(fakeUser.accessToken);

    //         console.log("Токен збережено в LocalStorage");

    //         goto('/profile');
            
    //     } catch (err: any) {
    //         errorMsg = "Помилка";
    //     } finally {
    //         isLoading = false;
    //     }
    // }
</script>

<div class="flex items-center justify-center min-h-[80vh]">
	<div class="w-full max-w-md bg-bkg-header p-8 rounded-xl shadow-2xl border border-gray-800">
		
		<h1 class="text-3xl font-black text-white mb-6 text-center tracking-wide">
			Вхід в <span class="text-brand-accent">Diploma</span>
		</h1>

		{#if errorMsg}
			<div class="mb-4 p-3 rounded bg-red-500/10 border border-red-500/50 text-red-200 text-sm text-center">
				{errorMsg}
			</div>
		{/if}

		<form onsubmit={handleLogin} class="space-y-5">
			<div>
				<label for="email" class="block text-sm font-medium text-text-muted mb-1">Email</label>
				<input 
					id="email"
					type="email" 
					bind:value={email}
					required
					placeholder="your@email.com"
					class="w-full bg-bkg-main text-white px-4 py-3 rounded-lg border border-gray-700 focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all placeholder-gray-600"
				/>
			</div>

			<div>
				<label for="password" class="block text-sm font-medium text-text-muted mb-1">Пароль</label>
				<input 
					id="password"
					type="password" 
					bind:value={password}
					required
					placeholder="••••••••"
					class="w-full bg-bkg-main text-white px-4 py-3 rounded-lg border border-gray-700 focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all placeholder-gray-600"
				/>
			</div>

			<button 
				type="submit" 
				disabled={isLoading}
				class="w-full bg-brand-accent hover:bg-brand-hover text-white font-bold py-3 rounded-lg transition-all transform active:scale-95 disabled:opacity-50 disabled:cursor-not-allowed shadow-lg shadow-brand-accent/20"
			>
				{#if isLoading}
					Вхід...
				{:else}
					Увійти
				{/if}
			</button>
		</form>

		<div class="mt-6 text-center text-sm text-text-muted">
			Ще не маєте акаунту? 
			<a href="/auth/register" class="text-white hover:text-brand-accent font-semibold transition-colors">
				Зареєструватися
			</a>
		</div>
	</div>
</div>