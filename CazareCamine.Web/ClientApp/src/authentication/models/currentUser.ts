export interface CurrentUser {
  firstName: string,
  lastName: string,
  email: string,
  roles: Array<string>,
  isAuthenticated: boolean
}
