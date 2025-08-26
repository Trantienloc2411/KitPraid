export interface IUser {
  id: string;
  name: string;
  email: string;
  password: string;
  createdAt: Date;
  updatedAt: Date;
  token?: string;
  role: string;
  isActive: boolean;
  isVerified: boolean;
  isAdmin: boolean;
  isSuperAdmin: boolean;
  isStaff: boolean;
  isCustomer: boolean;
}

export interface IUserResponse {
    token?: string;
    expiresIn?: number;
    tokenType?: string;
    scope?: string;
}

export interface IUserLogin {
    email: string;
    password: string;
}
