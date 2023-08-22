import type { Metadata } from 'next';
import './globals.css';
import { Source_Code_Pro } from 'next/font/google';

const sourceCodePro = Source_Code_Pro({
  subsets: ['latin'],
  weight: ['300', '400', '500', '600', '700'],
  variable: '--font-sourcecodepro',
});

export const metadata: Metadata = {
  title: 'BattleCottage',
  description:
    'Yet another addition to the ever-growing collection of My Daily Routine apps, boldly claiming to outshine its lackluster predecessors like a disco ball at a power outage.',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body suppressHydrationWarning={true} className={`${sourceCodePro.variable}`}>
        {children}
      </body>
    </html>
  );
}
