import type { Config } from 'tailwindcss';

const config: Config = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      fontFamily: {
        roboto: ['var(--font-roboto)'],
        permanentmarker: ['var(--font-permanentmarker)'],
      },
      screens: {
        '3xl': '1600px',
      },
      keyframes: {
        press: {
          '0%': {
            transform: 'scale(1)',
          },
          '50%': {
            transform: 'scale(0.90)',
          },
          '100%': {
            transform: 'scale(1)',
          },
        },
      },
      animation: {
        press: 'press 0.1s ease-in-out',
      },
    },
  },
  plugins: [],
};
export default config;
