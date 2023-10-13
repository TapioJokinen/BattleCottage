/** @type {import('next').NextConfig} */
const isProd = process.env.NODE_ENV === 'production';
const nextConfig = {
    output: 'standalone',
    assetPrefix: isProd ? 'http://cdn.battlecottage.com' : undefined,
    images: {
        remotePatterns: [
            {
            protocol: 'http',
            hostname: 'cdn.battlecottage.com',
            port: '',
            pathname: '/images/**',
            },
        ],
        },
};

module.exports = nextConfig;
