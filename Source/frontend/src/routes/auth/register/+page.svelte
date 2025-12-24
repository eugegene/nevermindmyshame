<script lang="ts">
	import { goto } from '$app/navigation';
    // import { AuthService } from '$lib/services/auth.service'; // Розкоментуйте, коли додасте метод register

	let email = $state('');
	let password = $state('');
	let confirmPassword = $state('');
	let isLoading = $state(false);
	let errorMsg = $state('');

	async function handleRegister(e: Event) {
		e.preventDefault();
		errorMsg = '';

		if (password !== confirmPassword) {
			errorMsg = 'Паролі не співпадають';
			return;
		}

		isLoading = true;

		try {
			console.log('Реєстрація:', { email, password });
            
            await new Promise(r => setTimeout(r, 1000));
            
			goto('/auth/login');
		} catch (err: any) {
			errorMsg = err.body?.message || "Помилка реєстрації.";
		} finally {
			isLoading = false;
		}
	}
</script>

<div class="flex items-center justify-center min-h-[80vh]">
	<div class="w-full max-w-md bg-bkg-header p-8 rounded-xl shadow-2xl border border-gray-800">
		
		<h1 class="text-3xl font-black text-white mb-6 text-center tracking-wide">
			Створити акаунт
		</h1>

		{#if errorMsg}
			<div class="mb-4 p-3 rounded bg-red-500/10 border border-red-500/50 text-red-200 text-sm text-center">
				{errorMsg}
			</div>
		{/if}

		<form onsubmit={handleRegister} class="space-y-5">
			<div>
				<label for="email" class="block text-sm font-medium text-text-muted mb-1">Email</label>
				<input 
					id="email"
					type="email" 
					bind:value={email}
					required
					class="w-full bg-bkg-main text-white px-4 py-3 rounded-lg border border-gray-700 focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
			</div>

			<div>
				<label for="password" class="block text-sm font-medium text-text-muted mb-1">Пароль</label>
				<input 
					id="password"
					type="password" 
					bind:value={password}
					required
					class="w-full bg-bkg-main text-white px-4 py-3 rounded-lg border border-gray-700 focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
			</div>

            <div>
				<label for="confirm_password" class="block text-sm font-medium text-text-muted mb-1">Підтвердіть пароль</label>
				<input 
					id="confirm_password"
					type="password" 
					bind:value={confirmPassword}
					required
					class="w-full bg-bkg-main text-white px-4 py-3 rounded-lg border border-gray-700 focus:border-brand-accent focus:ring-1 focus:ring-brand-accent outline-none transition-all"
				/>
			</div>

			<button 
				type="submit" 
				disabled={isLoading}
				class="w-full bg-white hover:bg-gray-200 text-black font-bold py-3 rounded-lg transition-all transform active:scale-95 disabled:opacity-50"
			>
				{#if isLoading}
					Реєстрація...
				{:else}
					Створити акаунт
				{/if}
			</button>
		</form>

		<div class="mt-6 text-center text-sm text-text-muted">
			Вже є акаунт? 
			<a href="/auth/login" class="text-white hover:text-brand-accent font-semibold transition-colors">
				Увійти
			</a>
		</div>
	</div>
</div>