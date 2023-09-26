import LFGPostForm from '@/components/lfg/create/LFGPostForm';

export default function LFGCreatePage() {
  return (
    <div className="content-container">
      <div className="animated-border tool-title-wrapper">
        <span className="">Create your own LFG post!</span>
      </div>
      <div className="lfg-create-form-container">
        <LFGPostForm />
      </div>
    </div>
  );
}
