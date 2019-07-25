import { MenuItem, createItem } from "../basic";
import { TEXT } from "@/common/lang";
import { MenuActions } from "@/events/FloatMenuClickEvent";
import context from "@/common/context";
import { isImg } from "@/dom/utils";
import { getFirstComment, isViewComment } from "../utils";
import { isDynamicContent, getCleanParent } from "@/kooboo/utils";
import { setInlineEditor } from "@/components/richEditor";
import { KOOBOO_ID, KOOBOO_DIRTY } from "@/common/constants";
import { emitSelectedEvent, emitHoverEvent } from "@/dom/events";
import { KoobooComment } from "@/kooboo/KoobooComment";
import { createP } from "@/dom/element";

export function createReplaceToTextItem(): MenuItem {
  const { el, setVisiable } = createItem(TEXT.REPLACE_TO_TEXT, MenuActions.replaceToText);
  const update = (comments: KoobooComment[]) => {
    setVisiable(true);
    let args = context.lastSelectedDomEventArgs;
    let firstComment = getFirstComment(comments);
    if (!firstComment || !isViewComment(firstComment)) return setVisiable(false);
    let { koobooId, parent } = getCleanParent(args.element);
    if (!parent && !koobooId) return setVisiable(false);
    if (!isImg(args.element)) return setVisiable(false);
    if (parent && isDynamicContent(parent)) return setVisiable(false);
  };

  el.addEventListener("click", async () => {
    let args = context.lastSelectedDomEventArgs;
    let { parent } = getCleanParent(args.element);
    let startContent = parent!.innerHTML;
    try {
      let text = createP();
      let style = getComputedStyle(args.element);
      text.setAttribute(KOOBOO_ID, args.koobooId!);
      text.setAttribute(KOOBOO_DIRTY, "");
      text.style.width = style.width;
      text.style.height = style.height;
      text.style.display = style.display;
      args.element.parentElement!.replaceChild(text, args.element);
      emitHoverEvent(text);
      emitSelectedEvent();
      await setInlineEditor(text, startContent);
    } catch (error) {
      parent!.innerHTML = startContent;
    }
  });

  return { el, update };
}
