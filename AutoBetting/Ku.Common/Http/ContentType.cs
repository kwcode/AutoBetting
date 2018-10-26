using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Common
{
    public class ContentTypeKit
    {
        public enum ContentType
        {
            flash,  	//application/x-shockwave-flash
            html,	    //text/html
            jpg,    	//image/jpeg
            jpeg,   	//image/jpeg
            gif,    	//image/gif
            png,    	//image/png
            x_javascript,   	//application/x-javascript
            javascript, 	//text/javascript
            css,    	//text/css
            xml,	    //application/xml
            json,   	//application/json
            bmp,	    //image/bmp
            ajax,	    //application/ajax
            flex,	    //application/flex
            silverlight,    	//application/silverlight
            mp3,    	//audio/mpeg
            mpeg,   	//audio/mpeg
            mid,    	//audio/midi
            wav,    	//audio/x-wav
            mov,    	//video/quicktime
            exe,    	//application/octet-stream
            gz, 	//application/x-gzip
            pdb,	    //chemical/x-pdb
            pdf,	    //application/pdf
            ppt,	    //application/mspowerpoint
            ra,	    //audio/x-realaudio
            ram,	    //audio/x-pn-realaudio
            rm,	    //audio/x-pn-realaudio
            rtf,	    //text/rtf
            rtx,	    //text/richtext
            src,	    //application/x-wais-source
            tif,	    //image/tiff
            tiff,	    //image/tiff
            txt,	    //text/plain
            xls,	    //application/vnd.ms-excel
            zip 	//application/zip
        }

        public string getContentTypeString(ContentType ct)
        {
            switch (ct)
            {
                case ContentType.flash: return "application/x-shockwave-flash";
                case ContentType.html: return "text/html";
                case ContentType.jpg: return "image/jpeg";
                case ContentType.jpeg: return "image/jpeg";
                case ContentType.gif: return "image/gif";
                case ContentType.png: return "image/png";
                case ContentType.x_javascript: return "application/x-javascript";
                case ContentType.javascript: return "text/javascript";
                case ContentType.css: return "text/css";
                case ContentType.xml: return "application/xml";
                case ContentType.json: return "application/json";
                case ContentType.bmp: return "image/bmp";
                case ContentType.ajax: return "application/ajax";
                case ContentType.flex: return "application/flex";
                case ContentType.silverlight: return "application/silverlight";
                case ContentType.mp3: return "audio/mpeg";
                case ContentType.mpeg: return "audio/mpeg";
                case ContentType.mid: return "audio/midi";
                case ContentType.wav: return "audio/x-wav";
                case ContentType.mov: return "video/quicktime";
                case ContentType.exe: return "application/octet-stream";
                case ContentType.gz: return "application/x-gzip";
                case ContentType.pdb: return "chemical/x-pdb";
                case ContentType.pdf: return "application/pdf";
                case ContentType.ppt: return "application/mspowerpoint";
                case ContentType.ra: return "audio/x-realaudio";
                case ContentType.ram: return "audio/x-pn-realaudio";
                case ContentType.rm: return "audio/x-pn-realaudio";
                case ContentType.rtf: return "text/rtf";
                case ContentType.rtx: return "text/richtext";
                case ContentType.src: return "application/x-wais-source";
                case ContentType.tif: return "image/tiff";
                case ContentType.tiff: return "image/tiff";
                case ContentType.txt: return "text/plain";
                case ContentType.xls: return "application/vnd.ms-excel";
                case ContentType.zip: return "application/zip";
                default: return "text/html";
            }
        }
    }
}