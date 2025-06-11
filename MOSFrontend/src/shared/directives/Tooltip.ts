import type { Directive, DirectiveBinding } from "vue";

export type TPsiTooltipPosition = "left" | "top" | "right" | "bottom";

declare global {
  interface HTMLElement {
    psiTooltip?: {
      element: HTMLElement;
      text: string | null;
      position: string;
      offset: number;
      delay: number;
      showTimeout?: ReturnType<typeof setTimeout>;
      width: string;
    };
  }
}

interface TooltipSettings {
  text: string;
  position: TPsiTooltipPosition;
  offset: number;
  width: string;
  delay: number;
}

function parseSettings(value: string | TooltipSettings): TooltipSettings {
  if (typeof value === "string") {
    return {
      text: value,
      position: "bottom",
      offset: 10,
      width: "120px",
      delay: 500
    };
  }

  return {
    text: value.text,
    position: value.position ?? "bottom",
    offset: value.offset ?? 10,
    width: value.width ?? "120px",
    delay: value.delay ?? 500
  };
}

export const tooltipDirective: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    const settings = parseSettings(binding.value);

    const tooltipElement = document.createElement("div");
    document.body.appendChild(tooltipElement);

    tooltipElement.className = `psi-tooltip caption-regular`;

    el.psiTooltip = {
      element: tooltipElement,
      text: settings.text,
      offset: settings.offset,
      position: settings.position,
      delay: settings.delay,
      width: settings.width
    };

    el.addEventListener("mouseenter", showTooltip);
    el.addEventListener("mouseleave", hideTooltip);
    el.addEventListener("click", hideTooltip);
  },

  updated(el: HTMLElement, binding: DirectiveBinding) {
    const settings = parseSettings(binding.value);
    const tooltip = el.psiTooltip;
    if (tooltip) {
      tooltip.position = settings.position;
      tooltip.delay = settings.delay;
      tooltip.element.style.width = settings.width;

      tooltip.text = settings.text;
    }
  },

  unmounted(el: HTMLElement) {
    if (el.psiTooltip) {
      el.removeEventListener("mouseenter", showTooltip);
      el.removeEventListener("mouseleave", hideTooltip);
      el.removeEventListener("click", hideTooltip);

      if (el.psiTooltip.showTimeout) {
        el.psiTooltip.showTimeout;
      }
      el.psiTooltip.element.remove();
      delete el.psiTooltip;
    }
  }
};

function showTooltip(this: HTMLElement) {
  const tooltip = this.psiTooltip;
  if (!tooltip || !tooltip.text || tooltip.text.length == 0) {
    return;
  }

  // Меняем текст и ширину при отображении подсказки,
  // чтобы во время анимация не было резких изменений.
  tooltip.element.textContent = tooltip.text;
  tooltip.element.style.width = tooltip.width;

  tooltip.showTimeout = setTimeout(() => {
    positionTooltip(this, tooltip.element, tooltip.offset, tooltip.position);
    tooltip.element.classList.add("active");
  }, tooltip.delay);
}

function hideTooltip(this: HTMLElement) {
  const tooltip = this.psiTooltip;
  if (!tooltip) {
    return;
  }

  clearTimeout(tooltip.showTimeout);
  tooltip.element.classList.remove("active");
}

function positionTooltip(target: HTMLElement, tooltip: HTMLElement, offset: number, position: string) {
  const targetRect = target.getBoundingClientRect();
  const tooltipRect = tooltip.getBoundingClientRect();

  const viewportHeight = window.innerHeight;
  const viewportWidth = window.innerWidth;

  let topOffset: number = 0;
  let leftOffset: number = 0;

  if (position === "top") {
    const distanceY = tooltipRect.height + offset;
    if (distanceY < targetRect.top) {
      topOffset = -distanceY;
    }
    else {
      topOffset = targetRect.height + offset;
    }

    const viewportSpaceWidth = viewportWidth - targetRect.left;
    if (viewportSpaceWidth > tooltipRect.width) {
      leftOffset = targetRect.width / 2 - tooltipRect.width / 2;
    }
    else {
      leftOffset = viewportSpaceWidth - tooltipRect.width - 12;
    }
  }
  else if (position === "bottom") {
    const distanceY = tooltipRect.height + offset;
    const viewportSpaceHeight = viewportHeight - targetRect.top - targetRect.height;
    if (distanceY > viewportSpaceHeight) {
      topOffset = -distanceY;
    }
    else {
      topOffset = targetRect.height + offset;
    }

    const viewportSpaceWidth = viewportWidth - targetRect.left;
    if (viewportSpaceWidth > tooltipRect.width) {
      leftOffset = targetRect.width / 2 - tooltipRect.width / 2;
    }
    else {
      leftOffset = viewportSpaceWidth - tooltipRect.width - 12;
    }
  }
  else if (position === "left") {
    const viewportSpaceLeft = targetRect.left;
    if (viewportSpaceLeft > tooltipRect.width) {
      leftOffset = -(tooltipRect.width + offset);
    }
    else {
      leftOffset = targetRect.width + offset;
    }

    if (targetRect.top > tooltipRect.height / 2) {
      topOffset = targetRect.height / 2 - tooltipRect.height / 2;
    }
    else {
      topOffset = -targetRect.top;
    }
  }
  else {
    const viewportSpaceRight = viewportWidth - targetRect.left - targetRect.width;
    if (viewportSpaceRight > tooltipRect.width) {
      leftOffset = targetRect.width + offset;
    }
    else {
      leftOffset = -(tooltipRect.width + offset);
    }

    if (targetRect.top > tooltipRect.height / 2) {
      topOffset = targetRect.height / 2 - tooltipRect.height / 2;
    }
    else {
      topOffset = -targetRect.top;
    }
  }

  const scrollY = window.scrollY;
  const scrollX = window.scrollX;

  tooltip.style.top = targetRect.top + scrollY + topOffset + "px";
  tooltip.style.left = targetRect.left + scrollX + leftOffset + "px";
}
