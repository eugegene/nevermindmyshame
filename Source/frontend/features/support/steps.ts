import { Given, When, Then } from "@cucumber/cucumber";
import { expect } from "@playwright/test";
import { CustomWorld } from "./world";

// 1. Передумова
Given(
  "Гість знаходиться на сторінці {string}",
  async function (this: CustomWorld, url: string) {
    await this.page!.goto(`http://localhost:5173${url}`);
  }
);

// 2. Заповнення форми
When(
  "Гість вводить наступні валідні дані:",
  async function (this: CustomWorld, dataTable: any) {
    const data = dataTable.rowsHash();
    if (data["Email"])
      await this.page!.fill('input[type="email"]', data["Email"]);
    if (data["Пароль"])
      await this.page!.fill('input[type="password"]', data["Пароль"]);
  }
);

When(
  "Гість натискає кнопку {string}",
  async function (this: CustomWorld, buttonText: string) {
    await this.page!.getByRole("button", { name: buttonText }).click();
  }
);

Then(
  "Користувача перенаправлено на головну сторінку {string}",
  async function (this: CustomWorld, url: string) {
    await expect(this.page!).toHaveURL(`http://localhost:5173${url}`);
  }
);

Then(
  "Система створює нового користувача {string} в базі даних",
  async function () {
    console.log("TODO: Перевірити базу даних (поки пропускаємо)");
  }
);

Then("Користувач {string} автоматично авторизований", async function () {
  console.log("TODO: Перевірити токен в LocalStorage");
});
