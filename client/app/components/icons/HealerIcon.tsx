export default function HealerIcon({ className }: { className?: string }) {
  return (
    <svg
      fill="#388e3c"
      width="35px"
      height="35px"
      viewBox="0 0 24 24"
      className={className || 'h-8 w-8'}
      xmlns="http://www.w3.org/2000/svg"
    >
      <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
      <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
      <g id="SVGRepo_iconCarrier">
        {' '}
        <path
          fillRule="evenodd"
          d="M16,8 L19.7777778,8 C21.0050772,8 22,8.81402773 22,9.81818182 L22,14.1818182 C22,15.1859723 21.0050772,16 19.7777778,16 L16,16 L16,19.7777778 C16,21.0050772 15.1859723,22 14.1818182,22 L9.81818182,22 C8.81402773,22 8,21.0050772 8,19.7777778 L8,16 L4.22222222,16 C2.99492278,16 2,15.1859723 2,14.1818182 L2,9.81818182 C2,8.81402773 2.99492278,8 4.22222222,8 L8,8 L8,4.22222222 C8,2.99492278 8.81402773,2 9.81818182,2 L14.1818182,2 C15.1859723,2 16,2.99492278 16,4.22222222 L16,8 Z M14,10 L14,4 L10,4 L10,10 L4,10 L4,14 L10,14 L10,20 L14,20 L14,14 L20,14 L20,10 L14,10 Z"
        ></path>{' '}
      </g>
    </svg>
  );
}
