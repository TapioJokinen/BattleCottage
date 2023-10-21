import type { Metadata } from 'next';
import './globals.css';
import { Roboto, Permanent_Marker } from 'next/font/google';
import Providers from '@/context/Providers';

const permanent_marker = Permanent_Marker({
  subsets: ['latin'],
  weight: ['400'],
  variable: '--font-permanentmarker',
});

const roboto = Roboto({
  subsets: ['latin'],
  weight: ['300', '400', '500', '700'],
  variable: '--font-roboto',
});

export const metadata: Metadata = {
  title: 'BattleCottage',
  description:
    'Yet another addition to the ever-growing collection of My Daily Routine apps, boldly claiming to outshine its lackluster predecessors like a disco ball at a power outage.',
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="en">
      <body
        suppressHydrationWarning={true}
        className={`${roboto.variable} ${permanent_marker.variable} font-opensans`}
      >
        <Providers>{children}</Providers>
      </body>
    </html>
  );
}
