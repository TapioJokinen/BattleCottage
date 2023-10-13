/** @type {import('next').NextConfig} */
const isProd = process.env.NODE_ENV === 'production';
const nextConfig = {
    output: 'standalone',
    assetPrefix: isProd ? 'https://cdn.battlecottage.com' : undefined,
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: 'cdn.battlecottage.com',
                port: '',
                pathname: '/images/**',
            },
        ],
    },
    async headers() {
        return [
          {
            source: "/_next/:path*",
            headers: [
              { key: "Access-Control-Allow-Origin", value: "https://cdn.battlecottage.com" },
            ],
          },
        ]
      },
};

module.exports = nextConfig;
