'use client';

import { useState } from 'react';
import RoundedButton from './buttons/RoundedButton';
import SignInForm from './forms/SignInForm';
import RegisterForm from './forms/RegisterForm';

export default function AuthContainer() {
  const [isSignIn, setIsSignIn] = useState<boolean>(true);
  const [isRegister, setIsRegister] = useState<boolean>(false);

  function handleIsSignIn() {
    setIsSignIn(true);
    setIsRegister(false);
  }

  function handleIsRegister() {
    setIsSignIn(false);
    setIsRegister(true);
  }

  return (
    <div className="authcontainer-main">
      <div className="authcontainer-content">
        <div className="authcontainer-forms">
          <div className="authcontainer-forms-swicth">
            <RoundedButton text="Sign in" isActive={isSignIn} onClick={handleIsSignIn} />
            <RoundedButton text="Register" isActive={isRegister} onClick={handleIsRegister} />
          </div>
          {isSignIn && <SignInForm />}
          {isRegister && <RegisterForm />}
        </div>
        <p className="authcontainer-forms-footer ">
          Explore the top app for finding gaming partners! Share "Looking for Group" messages or
          browse a personalized list of potential companions. Boost your status in the Battle
          Cottage community through rating exchanges with friends!
        </p>
      </div>
    </div>
  );
}
