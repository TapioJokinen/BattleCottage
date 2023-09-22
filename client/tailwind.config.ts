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
        sourcecodepro: ['var(--font-sourcecodepro)'],
        opensans: ['var(--font-opensans)'],
      },
      screens: {
        '3xl': '1600px',
      },
      keyframes: {
        typing: {
          '0%': {
            width: '0%',
            visibility: 'hidden',
          },
          '100%': {
            width: '100%',
          },
        },
        blink: {
          '50%': {
            borderColor: 'transparent',
          },
          '100%': {
            borderColor: 'white',
          },
        },
      },
      animation: {
        typing: 'typing 2s steps(30) alternate, blink .7s infinite',
      },
      boxShadow: {
        sliding_main: 'inset 0 0 0 0 var(--palette-orange)',
        right_main: 'inset 425px 0 0 0 var(--palette-orange)',
        sliding_secondary: 'inset 0 0 0 0 var(--palette-grey)',
        right_secondary: 'inset 425px 0 0 0 var(--palette-grey)',
      },
    },
  },
  plugins: [],
};
export default config;
