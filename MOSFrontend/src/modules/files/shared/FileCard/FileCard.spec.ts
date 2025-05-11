import { FileKind, IUploadedFile } from "@/shared/types";
import FileCard from "@/modules/files/shared/FileCard/FileCard.vue";
import { mount } from "@vue/test-utils";

const mockFile: IUploadedFile = {
  id: 1,
  originalName: "document.pdf",
  storedName: "43865a08-a68d-4dba-8ffd-938238c19ac7.png",
  url: "http://example.com/file.pdf",
  uploadDate: "2024-10-05T12:00:00Z",
  type: "",
  kind: FileKind.Image,
  size: 2
};

describe("FileCard", () => {
  it("renders file name and date", () => {
    const wrapper = mount(FileCard, {
      props: { file: mockFile }
    });

    expect(wrapper.text()).toContain("document.pdf");
    expect(wrapper.text()).toContain("43865a08-a68d-4dba-8ffd-938238c19ac7.png");
    expect(wrapper.text()).toContain("2024.10.05 12:00");
  });

  /* it("copies URL on click", async () => {
    Object.assign(navigator, {
      clipboard: {
        writeText: vi.fn()
      }
    });

    const wrapper = mount(FileCard, {
      props: { file: mockFile }
    });

    await wrapper.trigger("click");
    expect(navigator.clipboard.writeText).toHaveBeenCalledWith("http://example.com/file.pdf");
  });

  it("emits delete event", async () => {
    const wrapper = mount(FileCard, {
      props: { file: mockFile }
    });

    await wrapper.findComponent({ name: "PsiButton" }).trigger("click");
    expect(wrapper.emitted("delete")).toBeTruthy();
  }); */
});
