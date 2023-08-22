export default function Header({
  textMain,
  textSecondary,
}: {
  textMain: string;
  textSecondary: string;
}) {
  return (
    <div className="mt-6 flex w-full justify-center font-sourcecodepro text-[#e7e7e7] md:mt-20">
      <div className="flex max-w-[600px] flex-col items-center justify-center p-2 pt-5">
        <div className="flex p-2 pb-5 md:pb-10">
          {textMain && <span className="typed-text">{textMain}</span>}
        </div>
        {textSecondary && (
          <span className="text-center text-sm italic md:text-base">{textSecondary}</span>
        )}
      </div>
    </div>
  );
}
