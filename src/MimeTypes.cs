using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Dictionary of Mime Types by File Extension.
/// </summary>
public static class MimeTypes
{
    public static Dictionary<string, string> MimeTypeDictionary = new Dictionary<string, string>
    {
        {".ai"     ,"application/postscript"},
        {".aif"    ,"audio/x-aiff"},
        {".aifc"   ,"audio/x-aiff"},
        {".aiff"   ,"audio/x-aiff"},
        {".asc"    ,"text/plain"},
        {".atom"   ,"application/atom+xml"},
        {".au"     ,"audio/basic"},
        {".avi"    ,"video/x-msvideo"},
        {".bcpio"  ,"application/x-bcpio"},
        {".bin"    ,"application/octet-stream"},
        {".bmp"    ,"image/bmp"},
        {".cdf"    ,"application/x-netcdf"},
        {".cgm"    ,"image/cgm"},
        {".class"  ,"application/octet-stream"},
        {".cpio"   ,"application/x-cpio"},
        {".cpt"    ,"application/mac-compactpro"},
        {".csh"    ,"application/x-csh"},
        {".css"    ,"text/css"},
        {".dcr"    ,"application/x-director"},
        {".dif"    ,"video/x-dv"},
        {".dir"    ,"application/x-director"},
        {".djv"    ,"image/vnd.djvu"},
        {".djvu"   ,"image/vnd.djvu"},
        {".dll"    ,"application/octet-stream"},
        {".dmg"    ,"application/octet-stream"},
        {".dms"    ,"application/octet-stream"},
        {".doc"    ,"application/msword"},
        {".docm"   ,"application/vnd.ms-word.document.macroEnabled.12"},
        {".docx"   ,"application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
        {".dotm"   ,"application/vnd.ms-word.template.macroEnabled.12"},
        {".dotx"   ,"application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
        {".dtd"    ,"application/xml-dtd"},
        {".dv"     ,"video/x-dv"},
        {".dvi"    ,"application/x-dvi"},
        {".dxr"    ,"application/x-director"},
        {".eps"    ,"application/postscript"},
        {".etx"    ,"text/x-setext"},
        {".exe"    ,"application/octet-stream"},
        {".ez"     ,"application/andrew-inset"},
        {".gif"    ,"image/gif"},
        {".gram"   ,"application/srgs"},
        {".grxml"  ,"application/srgs+xml"},
        {".gtar"   ,"application/x-gtar"},
        {".hdf"    ,"application/x-hdf"},
        {".hqx"    ,"application/mac-binhex40"},
        {".htm"    ,"text/html"},
        {".html"   ,"text/html"},
        {".ice"    ,"x-conference/x-cooltalk"},
        {".ico"    ,"image/x-icon"},
        {".ics"    ,"text/calendar"},
        {".ief"    ,"image/ief"},
        {".ifb"    ,"text/calendar"},
        {".iges"   ,"model/iges"},
        {".igs"    ,"model/iges"},
        {".jnlp"   ,"application/x-java-jnlp-file"},
        {".jp2"    ,"image/jp2"},
        {".jpe"    ,"image/jpeg"},
        {".jpeg"   ,"image/jpeg"},
        {".jpg"    ,"image/jpeg"},
        {".js"     ,"application/x-javascript"},
        {".kar"    ,"audio/midi"},
        {".latex"  ,"application/x-latex"},
        {".lha"    ,"application/octet-stream"},
        {".lzh"    ,"application/octet-stream"},
        {".m3u"    ,"audio/x-mpegurl"},
        {".m4a"    ,"audio/mp4a-latm"},
        {".m4b"    ,"audio/mp4a-latm"},
        {".m4p"    ,"audio/mp4a-latm"},
        {".m4u"    ,"video/vnd.mpegurl"},
        {".m4v"    ,"video/x-m4v"},
        {".mac"    ,"image/x-macpaint"},
        {".man"    ,"application/x-troff-man"},
        {".mathml" ,"application/mathml+xml"},
        {".me"     ,"application/x-troff-me"},
        {".mesh"   ,"model/mesh"},
        {".mid"    ,"audio/midi"},
        {".midi"   ,"audio/midi"},
        {".mif"    ,"application/vnd.mif"},
        {".mov"    ,"video/quicktime"},
        {".movie"  ,"video/x-sgi-movie"},
        {".mp2"    ,"audio/mpeg"},
        {".mp3"    ,"audio/mpeg"},
        {".mp4"    ,"video/mp4"},
        {".mpe"    ,"video/mpeg"},
        {".mpeg"   ,"video/mpeg"},
        {".mpg"    ,"video/mpeg"},
        {".mpga"   ,"audio/mpeg"},
        {".ms"     ,"application/x-troff-ms"},
        {".msh"    ,"model/mesh"},
        {".mxu"    ,"video/vnd.mpegurl"},
        {".nc"     ,"application/x-netcdf"},
        {".oda"    ,"application/oda"},
        {".ogg"    ,"application/ogg"},
        {".pbm"    ,"image/x-portable-bitmap"},
        {".pct"    ,"image/pict"},
        {".pdb"    ,"chemical/x-pdb"},
        {".pdf"    ,"application/pdf"},
        {".pgm"    ,"image/x-portable-graymap"},
        {".pgn"    ,"application/x-chess-pgn"},
        {".pic"    ,"image/pict"},
        {".pict"   ,"image/pict"},
        {".png"    ,"image/png"},
        {".pnm"    ,"image/x-portable-anymap"},
        {".pnt"    ,"image/x-macpaint"},
        {".pntg"   ,"image/x-macpaint"},
        {".pot"    ,"application/vnd.ms-powerpoint"},
        {".potm"   ,"application/vnd.ms-powerpoint.template.macroEnabled.12"},
        {".potx"   ,"application/vnd.openxmlformats-officedocument.presentationml.template"},
        {".ppa"    ,"application/vnd.ms-powerpoint"},
        {".ppam"   ,"application/vnd.ms-powerpoint.addin.macroEnabled.12"},
        {".ppm"    ,"image/x-portable-pixmap"},
        {".pps"    ,"application/vnd.ms-powerpoint"},
        {".ppsm "  ,"application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
        {".ppsx"   ,"application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
        {".ppt"    ,"application/vnd.ms-powerpoint"},
        {".pptm"   ,"application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
        {".pptx"   ,"application/vnd.openxmlformats-officedocument.presentationml.presentation"},
        {".ps"     ,"application/postscript"},
        {".qt"     ,"video/quicktime"},
        {".qti"    ,"image/x-quicktime"},
        {".qtif"   ,"image/x-quicktime"},
        {".ra"     ,"audio/x-pn-realaudio"},
        {".ram"    ,"audio/x-pn-realaudio"},
        {".ras"    ,"image/x-cmu-raster"},
        {".rdf"    ,"application/rdf+xml"},
        {".rgb"    ,"image/x-rgb"},
        {".rm"     ,"application/vnd.rn-realmedia"},
        {".roff"   ,"application/x-troff"},
        {".rtf"    ,"text/rtf"},
        {".rtx"    ,"text/richtext"},
        {".sgm"    ,"text/sgml"},
        {".sgml"   ,"text/sgml"},
        {".sh"     ,"application/x-sh"},
        {".shar"   ,"application/x-shar"},
        {".silo"   ,"model/mesh"},
        {".sit"    ,"application/x-stuffit"},
        {".skd"    ,"application/x-koan"},
        {".skm"    ,"application/x-koan"},
        {".skp"    ,"application/x-koan"},
        {".skt"    ,"application/x-koan"},
        {".smi"    ,"application/smil"},
        {".smil"   ,"application/smil"},
        {".snd"    ,"audio/basic"},
        {".so"     ,"application/octet-stream"},
        {".spl"    ,"application/x-futuresplash"},
        {".src"    ,"application/x-wais-source"},
        {".sv4cpio","application/x-sv4cpio"},
        {".sv4crc" ,"application/x-sv4crc"},
        {".svg"    ,"image/svg+xml"},
        {".swf"    ,"application/x-shockwave-flash"},
        {".t"      ,"application/x-troff"},
        {".tar"    ,"application/x-tar"},
        {".tcl"    ,"application/x-tcl"},
        {".tex"    ,"application/x-tex"},
        {".texi"   ,"application/x-texinfo"},
        {".texinfo","application/x-texinfo"},
        {".tif"    ,"image/tiff"},
        {".tiff"   ,"image/tiff"},
        {".tr"     ,"application/x-troff"},
        {".tsv"    ,"text/tab-separated-values"},
        {".txt"    ,"text/plain"},
        {".ustar"  ,"application/x-ustar"},
        {".vcd"    ,"application/x-cdlink"},
        {".vrml"   ,"model/vrml"},
        {".vxml"   ,"application/voicexml+xml"},
        {".wav"    ,"audio/x-wav"},
        {".wbmp"   ,"image/vnd.wap.wbmp"},
        {".wbmxl"  ,"application/vnd.wap.wbxml"},
        {".wml"    ,"text/vnd.wap.wml"},
        {".wmlc"   ,"application/vnd.wap.wmlc"},
        {".wmls"   ,"text/vnd.wap.wmlscript"},
        {".wmlsc"  ,"application/vnd.wap.wmlscriptc"},
        {".wrl"    ,"model/vrml"},
        {".xbm"    ,"image/x-xbitmap"},
        {".xht"    ,"application/xhtml+xml"},
        {".xhtml"  ,"application/xhtml+xml"},
        {".xla"    ,"application/vnd.ms-excel"},
        {".xlam"   ,"application/vnd.ms-excel.addin.macroEnabled.12"},
        {".xls"    ,"application/vnd.ms-excel"},
        {".xlsb"   ,"application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
        {".xlsm"   ,"application/vnd.ms-excel.sheet.macroEnabled.12"},
        {".xlsx"   ,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".xlt"    ,"application/vnd.ms-excel"},
        {".xltm"   ,"application/vnd.ms-excel.template.macroEnabled.12"},
        {".xltx"   ,"application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
        {".xml"    ,"application/xml"},
        {".xpm"    ,"image/x-xpixmap"},
        {".xsl"    ,"application/xml"},
        {".xslt"   ,"application/xslt+xml"},
        {".xul"    ,"application/vnd.mozilla.xul+xml"},
        {".xwd"    ,"image/x-xwindowdump"},
        {".xyz"    ,"chemical/x-xyz"},
        {".zip"    ,"application/zip"}
    };

    /// <summary>
    /// Returns the Dictionary entry that matches the Extension.
    /// </summary>
    public static KeyValuePair<string, string> FindByExtension(string Extension)
    {
        return MimeTypeDictionary.SingleOrDefault(oo => oo.Key.ToLowerInvariant() == Extension.ToLowerInvariant());
    }

    /// <summary>
    /// Returns the MimeType that matches the Extension. If no match is found an error is thrown.
    /// </summary>
    public static string GetMimeType(string Extension)
    {
        var rslt = FindByExtension(Extension);
        if (!string.IsNullOrWhiteSpace(rslt.Value))
            return rslt.Value;
        else
            throw new ApplicationException("Unknown Extension.");
    }

    /// <summary>
    /// Returns the MimeType that matches the Extension. If no match is found the default value is returned.
    /// </summary>
    public static string GetMimeTypeOrDefault(string Extension, string Default)
    {
        var rslt = FindByExtension(Extension);
        if (!string.IsNullOrWhiteSpace(rslt.Value))
            return rslt.Value;
        else
            return Default;
    }


    /// <summary>
    /// Returns the Dictionary entry that matches the MimeType.
    /// </summary>
    private static KeyValuePair<string, string> FindByMimeType(string MimeType)
    {
        return MimeTypeDictionary.SingleOrDefault(oo => oo.Value.ToLowerInvariant() == MimeType.ToLowerInvariant());
    }

    /// <summary>
    /// Returns the Extension that matches the MimeType. If no match is found an error is thrown.
    /// </summary>
    public static string GetExtension(string MimeType)
    {
        var rslt = FindByMimeType(MimeType);
        if (!string.IsNullOrWhiteSpace(rslt.Key))
            return rslt.Key;
        else
            throw new ApplicationException("Unknown Mime Type.");
    }

    /// <summary>
    /// Returns the Extension that matches the MimeType. If no match is found the default value is returned.
    /// </summary>
    public static string GetExtensionOrDefault(string MimeType, string Default)
    {
        var rslt = FindByMimeType(MimeType);
        if (!string.IsNullOrWhiteSpace(rslt.Key))
            return rslt.Key;
        else
            return Default;
    }

}