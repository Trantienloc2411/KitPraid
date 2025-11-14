import Input from './Input'
import Button from './Button'
import Link from './Link'
import { useState } from 'react'
const Newsletter = ({
    onSubscribe
}) => {
    const [email, setEmail] = useState('')
    const handleSubscribe = () => {
        if (onSubscribe) {
            onSubscribe(email)
        } else {
            console.log('Subscribe', email)
        }
        setEmail('')
    }
        return (
        <div className="newsletter-container" style={{ padding: '4rem clamp(1rem, 6vw, 10rem)', backgroundColor: '#1B6392', color: 'white' }}>
            <div className="newsletter-header" style={{ display: 'flex', flexDirection: 'column', gap: '1rem', alignItems: 'center', justifyContent: 'center' }}>
                <h2>Subscribe to our newsletter</h2>
                <p>Get the latest news and updates delivered to your inbox.<br/> Stay up to date with our latest products, promotions, and more.</p>
            </div>
            <div className="newsletter-form" style={{padding: '2rem 0', display: 'flex', flexDirection: 'row', gap: '1rem', alignItems: 'center', justifyContent: 'center' }}>
                <Input placeholder="Enter your email" size="lg" style={{ width: '50%' }} value={email} onChange={(e) => setEmail(e.target.value)}/>
                <Button label="Subscribe" size="lg" onClick={handleSubscribe}/>
            </div>
            <p style={{ textAlign: 'center' }}>By subscribing, you agree to our <Link href="#" onClick={() => console.log('Terms of Service')}>Terms of Service</Link> and <Link href="#" onClick={() => console.log('Privacy Policy')}>Privacy Policy</Link>.</p>
        </div>
    )
}

export default Newsletter;