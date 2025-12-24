export default {
  default: {
    // Де шукати сценарії
    paths: ["features/**/*.feature"],

    // Де шукати код. TypeScript завантажиться автоматично завдяки команді в package.json
    import: ["features/support/**/*.ts"],

    format: ["progress-bar"],

    worldParameters: {
      appUrl: "http://localhost:5173",
    },
  },
};
