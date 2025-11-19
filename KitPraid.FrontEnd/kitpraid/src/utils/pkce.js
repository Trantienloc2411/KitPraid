/**
 * PKCE (Proof Key for Code Exchange) Utility
 * Generates code verifier and code challenge for OAuth 2.0 Authorization Code flow with PKCE
 */

/**
 * Generate a cryptographically random string
 * @param {number} length - Length of the string
 * @returns {string} Random string
 */
function generateRandomString(length) {
  const charset = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~';
  let randomString = '';
  const randomValues = new Uint8Array(length);
  crypto.getRandomValues(randomValues);
  
  for (let i = 0; i < length; i++) {
    randomString += charset[randomValues[i] % charset.length];
  }
  
  return randomString;
}

/**
 * Generate code verifier (43-128 characters)
 * @returns {string} Code verifier
 */
function generateCodeVerifier() {
  // Generate a random string between 43 and 128 characters
  const length = Math.floor(Math.random() * (128 - 43 + 1)) + 43;
  return generateRandomString(length);
}

/**
 * Generate code challenge from code verifier using SHA-256
 * @param {string} codeVerifier - Code verifier string
 * @returns {Promise<string>} Base64URL encoded code challenge
 */
async function generateCodeChallenge(codeVerifier) {
  // Convert string to ArrayBuffer
  const encoder = new TextEncoder();
  const data = encoder.encode(codeVerifier);
  
  // Hash using SHA-256
  const hashBuffer = await crypto.subtle.digest('SHA-256', data);
  
  // Convert ArrayBuffer to base64url string
  const hashArray = Array.from(new Uint8Array(hashBuffer));
  const base64String = btoa(String.fromCharCode(...hashArray));
  
  // Convert base64 to base64url (replace + with -, / with _, remove padding)
  return base64String
    .replace(/\+/g, '-')
    .replace(/\//g, '_')
    .replace(/=/g, '');
}

/**
 * Generate PKCE pair (code verifier and code challenge)
 * @returns {Promise<{codeVerifier: string, codeChallenge: string}>}
 */
export async function generatePKCEPair() {
  const codeVerifier = generateCodeVerifier();
  const codeChallenge = await generateCodeChallenge(codeVerifier);
  
  return {
    codeVerifier,
    codeChallenge,
  };
}

export default {
  generatePKCEPair,
  generateCodeVerifier,
  generateCodeChallenge,
};

