export default function MidLaneIcon({ classname }: { classname?: string }) {
  return (
    <svg
      fill="#FFBB5C"
      width="35px"
      height="35px"
      viewBox="0 0 24 24"
      className={classname || 'h-8 w-8'}
      xmlns="http://www.w3.org/2000/svg"
    >
      <g id="SVGRepo_bgCarrier" strokeWidth="0"></g>
      <g id="SVGRepo_tracerCarrier" strokeLinecap="round" strokeLinejoin="round"></g>
      <g id="SVGRepo_iconCarrier">
        <path d="M3.293,20.707a1,1,0,0,1,0-1.414l16-16a1,1,0,1,1,1.414,1.414l-16,16A1,1,0,0,1,3.293,20.707Z"></path>
      </g>
    </svg>
  );
}
