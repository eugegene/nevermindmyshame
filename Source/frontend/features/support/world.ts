import { setWorldConstructor, World, IWorldOptions } from "@cucumber/cucumber";
import { Browser, BrowserContext, Page, chromium } from "@playwright/test";

export class CustomWorld extends World {
  browser: Browser | null = null;
  context: BrowserContext | null = null;
  page: Page | null = null;

  appUrl: string;

  constructor(options: IWorldOptions) {
    super(options);
    this.appUrl = options.parameters.appUrl || "http://localhost:5173";
  }

  async openBrowser() {
    this.browser = await chromium.launch({ headless: false });
    this.context = await this.browser.newContext();
    this.page = await this.context.newPage();
  }

  async closeBrowser() {
    await this.page?.close();
    await this.context?.close();
    await this.browser?.close();
  }
}

setWorldConstructor(CustomWorld);
