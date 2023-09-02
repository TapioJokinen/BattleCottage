import Header from '@/components/home/Header';
import AuthNav from '@/components/home/AuthNav';

function getHeader() {
  return (
    <Header
      textMain="🔥 Battle Cottage 🔥"
      textSecondary={`Discover the ultimate app for connecting with gaming partners! 
      Easily post your "Looking for Group" messages in a centralized hub or explore a curated list of potential gaming companions tailored to your preferences. 
      Engage in rating exchanges with your gaming buddies to enhance your standing within the Battle Cottage community!`}
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
