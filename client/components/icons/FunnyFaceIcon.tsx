export default function FunnyFaceIcon({ classname }: { classname?: string }) {
  return (
    <svg
      width="35px"
      height="35px"
      viewBox="0 0 24 24"
      className={classname || 'h-8 w-8'}
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
    >
      <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
      <g id="SVGRepo_tracerCarrier" strokeLinecap="round" stroke-linejoin="round"></g>
      <g id="SVGRepo_iconCarrier">
        {' '}
        <circle cx="12" cy="12" r="10" stroke="#FFBB5C" strokeWidth="1.5"></circle>{' '}
        <path
          d="M8.9126 15.9336C10.1709 16.249 11.5985 16.2492 13.0351 15.8642C14.4717 15.4793 15.7079 14.7653 16.64 13.863"
          stroke="#FFBB5C"
          stroke-width="1.5"
          stroke-linecap="round"
        ></path>{' '}
        <ellipse
          cx="14.5094"
          cy="9.77405"
          rx="1"
          ry="1.5"
          transform="rotate(-15 14.5094 9.77405)"
          fill="#FFBB5C"
        ></ellipse>{' '}
        <ellipse
          cx="8.71402"
          cy="11.3278"
          rx="1"
          ry="1.5"
          transform="rotate(-15 8.71402 11.3278)"
          fill="#FFBB5C"
        ></ellipse>{' '}
        <path
          d="M13 16.0004L13.478 16.9742C13.8393 17.7104 14.7249 18.0198 15.4661 17.6689C16.2223 17.311 16.5394 16.4035 16.1708 15.6524L15.7115 14.7168"
          stroke="#FFBB5C"
          stroke-width="1.5"
        ></path>{' '}
      </g>
    </svg>
  );
}
