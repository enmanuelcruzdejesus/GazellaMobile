using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GazellaMobile.Droid
{
    public class ButtonEditText : EditText
    {
        private Drawable _dleft, _dRight;
        private Rect _lBounds, _rBounds;


        public EventHandler RightClick { get; set; }
        public EventHandler LeftClick { get; set; }
        public ButtonEditText(Context context) : base(context) { }
        public ButtonEditText(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public ButtonEditText(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) { }
        public ButtonEditText(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {

        }


        void Initialize()
        {
            var imgName = "searchicon";
            int resImage = Resources.GetIdentifier(imgName, "drawable", Context.PackageName);
            this.SetCompoundDrawablesWithIntrinsicBounds(0, 0, resImage, 0);
        }

        public override void SetCompoundDrawables(Drawable left, Drawable top, Drawable right, Drawable bottom)
        {
            if (left != null)
            {
                _dleft = left;
            }
            if (right != null)
            {
                _dRight = right;
            }
            base.SetCompoundDrawables(left, top, right, bottom);
        }


        public override bool OnTouchEvent(MotionEvent e)
        {

            int x = (int)e.GetX();
            int y = (int)e.GetY();

            if (e.ActionMasked == MotionEventActions.Up && _dleft != null)
            {
                _lBounds = _dleft.Bounds;
                int n1 = this.Left;
                int n2 = this.Left + _lBounds.Width();
                int n3 = this.PaddingTop;
                int n4 = this.Height - this.PaddingBottom;
                //leva strana
                if (x >= (this.Left) && x <= (this.Left + _lBounds.Width())
                     && y >= this.PaddingTop
                     && y <= (this.Height - this.PaddingBottom))
                {
                    OnLeftClick();
                    e.Action = MotionEventActions.Cancel; // use this to prevent the keyboard to coming up

                }


            }

            if (e.Action == MotionEventActions.Up && _dRight != null)
            {
                _rBounds = _dRight.Bounds;
                int n1 = this.Right - _rBounds.Width();
                int n2 = this.Right - this.PaddingRight;
                int n3 = this.PaddingTop;
                int n4 = this.Height - this.PaddingBottom;

                //prava strana
                if (x >= (this.Right - _rBounds.Width()) && x <= (this.Right - PaddingRight)
                     && y >= this.PaddingTop && y <= (this.Height - this.PaddingBottom))
                {

                    OnRightClick();
                    e.Action = MotionEventActions.Cancel; // use this to prent the keyboard to coming up
                }
            }
            return base.OnTouchEvent(e);
        }

        void OnRightClick()
        {
            if (RightClick != null && _dRight != null)
            {
                RightClick(this, new EventArgs());
            }
        }

        void OnLeftClick()
        {
            if (LeftClick != null && _dleft != null)
            {
                LeftClick(this, new EventArgs());
            }
        }

        protected override void Dispose(bool disposing)
        {
            _dRight = null;
            _dleft = null;
            RightClick = null;
            LeftClick = null;
            base.Dispose(disposing);
        }


    }
}