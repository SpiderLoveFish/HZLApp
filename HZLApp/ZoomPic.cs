using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace HZLApp
{
   public class ZoomPic
    {
       LogHelper lh = new LogHelper();
        /// <summary> 
        /// 获取等比例缩放图片的方法 
        /// </summary> 
        /// <param name="imgPath">待缩放图片路径</param> 
        /// <param name="savePath">缩放图片保存路径</param> 
        /// <param name="format">缩放图片保存的格式</param> 
        /// <param name="scaling">要保持的宽度或高度</param> 
        /// <param name="keepWidthOrHeight">如果为true则保持宽度为scaling，否则保持高度为scaling</param> 
        /// <returns></returns> 
        public bool GetThumbnail(string imgPath, string savePath, ImageFormat format, int scaling, bool keepWidthOrHeight)
        {
            try
            {
                using (Bitmap myBitmap = new Bitmap(imgPath))
                {
                    int width = 0;
                    int height = 0;
                    int tw = myBitmap.Width;//图像的实际宽度 
                    int th = myBitmap.Height;//图像的实际高度 
                    if (keepWidthOrHeight)//保持宽度 
                    {
                        #region 自动保持宽度
                        if (scaling >= tw)
                        {
                            width = tw;
                            height = th;
                        }
                        else
                        {
                            double ti = Convert.ToDouble(tw) / Convert.ToDouble(scaling);
                            if (ti == 0d)
                            {
                                width = tw;
                                height = th;
                            }
                            else
                            {
                                width = scaling;
                                height = Convert.ToInt32(Convert.ToDouble(th) / ti);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 自动保持高度
                        if (scaling >= th)
                        {
                            width = tw;
                            height = th;
                        }
                        else
                        {
                            double ti = Convert.ToDouble(th) / Convert.ToDouble(scaling);
                            if (ti == 0d)
                            {
                                width = tw;
                                height = th;
                            }
                            else
                            {
                                width = Convert.ToInt32(Convert.ToDouble(tw) / ti);
                                height = scaling;
                            }
                        }
                        #endregion
                    }
                    using (Image myThumbnail = myBitmap.GetThumbnailImage(width, height, () => { return false; }, IntPtr.Zero))
                    {
                        myThumbnail.Save(savePath, format);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                lh.wrirteLog("预览图","-生成预览图",ex.Message);
                return false;
            }
        } 
    }
}
