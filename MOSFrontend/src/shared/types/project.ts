export interface IProjectCompact {
  id: number;
  title: string;
  description: string;
  visible: boolean;
  createdAt?: string | Date;
  updatedAt?: string | Date | null;
}

export interface IProject {
  id: number;
  title: string;
  description: string;
  visible: boolean;
  createdAt?: string | Date;
  updatedAt?: string | Date | null;
}
