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
                source: "/:path*",
                headers: [
                    { key: "Access-Control-Allow-Credentials", value: "true" },
                    { key: "Access-Control-Allow-Origin", value: "cdn.battlecottage.com" }, // replace this your actual origin
                    { key: "Access-Control-Allow-Methods", value: "GET,DELETE,PATCH,POST,PUT" },
                    { key: "Access-Control-Allow-Headers", value: "X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Content-Type, Date, X-Api-Version" },
                ]
            }
        ]
    }
};

module.exports = nextConfig;
