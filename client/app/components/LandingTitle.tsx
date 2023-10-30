import Image from 'next/image';

export default function LandingTitle() {
  return (
    <div className="landingtitle-main">
      <div className="landingtitle-image-wrapper">
        <Image
          src={process.env.NEXT_PUBLIC_IMAGES_URL + '/battlecottage.png'}
          fill
          sizes="(max-width: 200px) 100vw" // TODO: No idea what this does. It's here just to remove warning.
          alt="Battle Cottage"
          className="!relative m-1 rounded-full"
        />
      </div>
      <span className="landingtitle-text">Battle&#160;Cottage</span>
    </div>
  );
}
