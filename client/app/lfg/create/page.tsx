import LfgPostForm from '@/components/lfg/create/LfgPostForm';

export default function LFGCreatePage() {
  return (
    <div className="content-container">
      <div className="animated-border tool-title-wrapper">
        <span className="">Create your own LFG post!</span>
      </div>
      <div className="lfg-create-form-container">
        <LfgPostForm />
      </div>
    </div>
  );
}
