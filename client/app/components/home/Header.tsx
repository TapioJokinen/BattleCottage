export default function Header({
  textMain,
  textSecondary,
}: {
  textMain: string;
  textSecondary: string;
}) {
  return (
    <div className="header-container">
      <div className="header-text-container">
        <div className="header-title-wrapper">
          {textMain && <span className="typed-text">{textMain}</span>}
        </div>
        {textSecondary && <span className="header-secondary-text">{textSecondary}</span>}
      </div>
    </div>
  );
}
