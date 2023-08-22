import Link from 'next/link';

export default function AuthNav() {
  return (
    <div className="mt-20 flex w-full flex-col items-center font-sourcecodepro text-[var(--text-dark)] md:mt-48">
      <div className="flex h-full w-full flex-col items-center justify-center">
        <div className="flex w-9/12 max-w-[425px] flex-col items-center justify-center">
          <div className="button-slide-main">
            <svg
              className="icon-size-1 ml-3"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="currentColor"
              viewBox="0 0 16 20"
            >
              <path d="M14 7h-1.5V4.5a4.5 4.5 0 1 0-9 0V7H2a2 2 0 0 0-2 2v9a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2Zm-5 8a1 1 0 1 1-2 0v-3a1 1 0 1 1 2 0v3Zm1.5-8h-5V4.5a2.5 2.5 0 1 1 5 0V7Z" />
            </svg>
            <Link className="mr-3 w-full p-[0.5rem] text-center text-base sm:p-4" href="/login">
              Validate your existentialism
            </Link>
          </div>
          <div className="button-slide-secondary">
            <svg
              className="icon-size-1 ml-3"
              aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg"
              fill="currentColor"
              viewBox="0 0 20 18"
            >
              <path d="M6.5 9a4.5 4.5 0 1 0 0-9 4.5 4.5 0 0 0 0 9ZM8 10H5a5.006 5.006 0 0 0-5 5v2a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-2a5.006 5.006 0 0 0-5-5Zm11-3h-2V5a1 1 0 0 0-2 0v2h-2a1 1 0 1 0 0 2h2v2a1 1 0 0 0 2 0V9h2a1 1 0 1 0 0-2Z" />
            </svg>
            <Link className="mr-3 w-full p-[0.5rem] text-center text-base sm:p-4" href="/register">
              Begin Your Journey into Emptiness...
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
}
