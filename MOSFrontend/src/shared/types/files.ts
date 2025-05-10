import { IPagination } from "@/shared/types/base.ts";

export interface IStorageInfo {
  freeSpace: number;
  totalSize: number;
}

export interface IUploadedFile {
  id: number;
  originalName: string;
  storedName: string;
  url: string;
  size: number;
  uploadDate: string | Date;
  type: string;
  kind: FileKind;
}

export enum FileKind {
  Other = 0,
  Text,
  Image,
  Audio,
  Video,
  Archive
}

export interface IFilesPagination extends IPagination<IUploadedFile> {
}
