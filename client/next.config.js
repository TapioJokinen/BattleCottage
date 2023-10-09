/** @type {import('next').NextConfig} */
const nextConfig = {
  output: 'standalone',
  images: {
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'battlecottage.com',
      },
    ],
  },
};

module.exports = nextConfig;
