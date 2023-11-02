import CreateLFGPostContainer from '@/app/components/CreateLFGPostContainer';

export default function LFGCreate() {
  return (
    <main className="main bg-gradient">
      <div className="flex w-full justify-center p-5 font-permanentmarker text-[1.5em] sm:text-[2em]">
        <span className="curved-underline gradient-text">Create Your own LFG post!</span>
      </div>
      <CreateLFGPostContainer />
    </main>
  );
}
