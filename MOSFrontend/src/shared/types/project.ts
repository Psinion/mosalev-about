import { IPagination } from "@/shared/types/base.ts";

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

export interface IArticleCompact {
  id: number;
  projectId: number | null;
  project: IProjectCompact | null;
  title: string;
  description: string;
  visible: boolean;
  createdAt?: string | Date;
  updatedAt?: string | Date | null;
}

export interface IArticle {
  id: number;
  projectId: number | null;
  project: IProjectCompact | null;
  title: string;
  description: string;
  visible: boolean;
  createdAt?: string | Date;
  updatedAt?: string | Date | null;
}

export interface IArticlesPagination extends IPagination<IArticle> {
}
