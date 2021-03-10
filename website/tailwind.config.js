const colors = require("tailwindcss/colors.js");

module.exports = {
  purge: ['./pages/**/*.{js,ts,jsx,tsx}', './components/**/*.{js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        ensek: {
          blue: "#172c4e",
          orange: "#f66e3e"
        }
      }
    },
  },
  variants: {
    extend: {
      backgroundColor: ["disabled"],
      cursor: ["disabled"]
    },
  },
  plugins: [],
}
