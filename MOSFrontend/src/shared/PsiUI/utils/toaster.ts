import { toast, ToastContainerOptions } from "vue3-toastify";
import { h } from "vue";
import PsiToast from "@/shared/PsiUI/components/PsiToast/PsiToast.vue";

export interface IToaster {
  clear: () => void;
  neutral: (header: string, description?: string) => void;
  success: (header: string, description?: string) => void;
  error: (header: string, description?: string) => void;
}

export const toasterDefaultGlobalOptions = {
  transition: toast.TRANSITIONS.SLIDE,
  autoClose: 3000,
  hideProgressBar: true,
  clearOnUrlChange: false,
  position: toast.POSITION.TOP_RIGHT,
  theme: "dark",
  icon: false,
  limit: 3
} as ToastContainerOptions;

class Toaster implements IToaster {
  clear(): void {
    toast.clearAll();
  }

  neutral(header: string, description?: string): void {
    toast(
      h(PsiToast, {
        header: header,
        description: description
      })
    );
  }

  success(header: string, description?: string): void {
    toast.success(
      h(PsiToast, {
        icon: "success",
        header: header,
        description: description,
        theme: "success"
      })
    );
  }

  error(header: string, description?: string): void {
    toast.error(
      h(PsiToast, {
        icon: "error",
        header: header,
        description: description,
        theme: "critical"
      })
    );
  }
}

const toasterInstance = new Toaster();

export function useToaster(): IToaster {
  return toasterInstance;
}
