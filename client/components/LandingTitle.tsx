import Image from 'next/image';

export default function LandingTitle() {
  return (
    <div className="mx-[200px] flex flex-col items-center justify-center text-white sm:mt-5">
      <div className="relative w-full min-w-[140px] max-w-[200px]">
        <Image
          src={process.env.NEXT_PUBLIC_IMAGES_URL + '/battlecottage.png'}
          fill
          sizes="(max-width: 200px) 100vw" // TODO: No idea what this does. It's here just to remove warning.
          alt="Battle Cottage"
          className="!relative m-1 rounded-full"
        />
      </div>
      <span className="font-permanentmarker break-keep text-[calc(2em+1.5vw)]">
        Battle&#160;Cottage
      </span>
    </div>
  );
}
