export default function BottomLaneIcon({ classname }: { classname?: string }) {
  return (
    <svg
      className={classname || 'h-6 w-6'}
      xmlns="http://www.w3.org/2000/svg"
      fill="#FFBB5C"
      stroke="#FFBB5C"
    >
      <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
      <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
      <g id="SVGRepo_iconCarrier">
        {' '}
        <polyline
          fill="none"
          stroke="#FFBB5C"
          strokeWidth="2"
          points="8 20 20 20 20 8"
        ></polyline>{' '}
      </g>
    </svg>
  );
}
