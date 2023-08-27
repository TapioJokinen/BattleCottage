import Link from 'next/link';
import LockIcon from '../icons/LockIcon';
import RegisterIcon from '../icons/RegisterIcon';

export default function AuthNav() {
  return (
    <div className="auth-nav-container">
      <div className="auth-nav-link-container">
        <div className="auth-nav-link-wrapper">
          <Link className="button-slide-main" href="/login">
            <LockIcon />
            <span className="auth-nav-link-text">Login</span>
          </Link>
          <Link className="button-slide-secondary" href="/register">
            <RegisterIcon />
            <span className="auth-nav-link-text">Register</span>
          </Link>
        </div>
      </div>
    </div>
  );
}
