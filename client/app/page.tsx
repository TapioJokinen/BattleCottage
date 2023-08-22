import Header from './components/home/Header';
import AuthNav from './components/home/AuthNav';

function getHeader() {
  return (
    <Header
      textMain="&#123; BattleCottage &#125;"
      textSecondary='Introducing the "BattleCottage" app &ndash; your digital partner in the mundane
            marathon of life. Experience the thrill of checking off tasks as they blend seamlessly
            into a canvas of monotony. Join us in a dance with the void, where every routine is a
            step closer to becoming a human hamster wheel champion...'
    />
  );
}

export default function Home() {
  return (
    <main className="main">
      {/* Mobile container */}
      <div className="flex h-full w-full flex-col sm:hidden">
        {getHeader()}
        <AuthNav />
      </div>

      {/* Tablet / Desktop container */}
      <div className="hidden h-full w-full flex-col sm:flex">
        <div className="flex items-center">{getHeader()}</div>
        <div className="flex items-start">
          <AuthNav />
        </div>
      </div>
    </main>
  );
}
