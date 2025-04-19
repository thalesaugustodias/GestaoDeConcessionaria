import Swal from 'sweetalert2';

type ToastType = 'success' | 'error' | 'warning' | 'info';

export function showToast(message: string, type: ToastType = 'success') {
  const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.addEventListener('mouseenter', Swal.stopTimer);
      toast.addEventListener('mouseleave', Swal.resumeTimer);
    }
  });

  Toast.fire({
    icon: type,
    title: message
  });
}

export const notifySuccess = (message: string) => showToast(message, 'success');
export const notifyError = (message: string) => showToast(message, 'error');
export const notifyWarning = (message: string) => showToast(message, 'warning');
export const notifyInfo = (message: string) => showToast(message, 'info');
