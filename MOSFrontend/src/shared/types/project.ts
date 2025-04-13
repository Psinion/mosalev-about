export interface IProjectCompact {
  id: number;
  title: string;
  description: string;
}

export interface IProject {
  id: number;
  title: string;
  description: string;
  createdAt?: string | Date;
  updatedAt?: string | Date | null;
}
