export default function TopLaneIcon({ classname }: { classname?: string }) {
  return (
    <svg
      className={classname || 'h-6 w-6'}
      xmlns="http://www.w3.org/2000/svg"
      fill="#FFBB5C"
      stroke="#FFBB5C"
    >
      <g id="SVGRepo_bgCarrier" stroke-width="0"></g>
      <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
      <g id="SVGRepo_iconCarrier">
        {' '}
        <polyline
          fill="none"
          stroke="#FFBB5C"
          strokeWidth="2"
          points="4 16 16 16 16 4"
          transform="rotate(180 10 10)"
        ></polyline>{' '}
      </g>
    </svg>
  );
}
