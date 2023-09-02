import ActionButton from '../buttons/ActionButton';
import ListIcon from '../icons/ListIcon';
import PlusIcon from '../icons/PlusIcon';

export default function CottageContent() {
  return (
    <div className="cottage-content-container">
      <div className="cottage-action-button-container">
        <ActionButton
          icon={<PlusIcon />}
          link="/lfg/create"
          primaryText="Looking for Group!"
          secondaryText='Create a new "LFG" post'
        />
        <ActionButton
          icon={<ListIcon />}
          link="/lfg"
          primaryText="My Groups"
          secondaryText='Handle your own "LFG" posts and groups you have requested to join'
        />
      </div>
      <div className="flex w-full justify-center">
        <div className="cottage-filter">
          <span>Filter</span>
        </div>
      </div>
      <div>
        <div>Boxes</div>
      </div>
    </div>
  );
}
