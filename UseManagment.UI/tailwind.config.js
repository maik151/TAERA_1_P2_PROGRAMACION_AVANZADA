/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        // Naranja "Material" personalizado
        primary: {
          50: '#fff7ed',
          100: '#ffedd5',
          500: '#f97316', // Tu color principal
          600: '#ea580c', // Para hover
          700: '#c2410c',
        },
        // Gris cálido para fondos
        surface: '#f8fafc', 
      },
      fontFamily: {
        sans: ['Roboto', 'sans-serif'], // Tipografía típica de Material (opcional)
      }
    },
  },
  plugins: [],
}