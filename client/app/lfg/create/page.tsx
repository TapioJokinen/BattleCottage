import OptionForm from '@/components/lfg/create/OptionForm';

export default function LFGCreatePage() {
  return (
    <div className="content-container">
      <span className="mt-4 font-sourcecodepro text-base lg:mt-5 lg:text-2xl">
        Create a "Looking for Group" -post
      </span>
      <div className="lfg-create-form-container">
        <OptionForm />
      </div>
    </div>
  );
}
