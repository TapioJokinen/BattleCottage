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
        input_label: {
          from: {
            transform: 'translate(18px, 12px)',
          },
          to: {
            transform: 'translate(10px, -8px)',
          },
        },
      },
      animation: {
        press: 'press 0.1s ease-in-out',
        input_label: 'input_label 0.3s forwards',
      },
    },
  },
};
export default config;
