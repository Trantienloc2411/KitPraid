import React from 'react';
import { useCountdown } from '../../utils/countdown';
import './CountdownTimer.css';

interface CountdownTimerProps {
  targetDate: Date;
  className?: string;
}

const CountdownTimer: React.FC<CountdownTimerProps> = ({ targetDate, className = '' }) => {
  const countdown = useCountdown(targetDate);

  return (
    <div className={`countdown-timer ${className}`}>
      <span className="countdown-label">Deal ends in:</span>
      <span className="countdown-time">
        {countdown.days}d {countdown.hours}h {countdown.minutes}m {countdown.seconds}s
      </span>
    </div>
  );
};

export default CountdownTimer;
