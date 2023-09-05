import Option from '@/components/lfg/create/Option';

export default function LFGCreatePage() {
  return (
    <div className="content-container">
      <div className="my-10 flex h-full w-full border-[1px] border-[var(--palette-dark-jungle-green-darker)] bg-[var(--palette-dark-jungle-green)]">
        <div className="flex w-full flex-col items-center p-10">
          <Option text="1. Choose Game">
            <span>list of games</span>
          </Option>
          <Option text="2. Give your post a Title">
            <span>The title</span>
          </Option>
          <Option text="3. Give your post a Description">
            <span>The description</span>
          </Option>
          <Option text="3. Select the Roles to be filled">
            <span>The roles</span>
          </Option>
        </div>
      </div>
    </div>
  );
}
