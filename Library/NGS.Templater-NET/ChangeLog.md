Templater change log
--------------------------

## 2021-12-28 v6.2.0

### Templater Editor improvements

 * Show details of input values in debug log. Specific types will show specialized windows/actions (image will open window showing the image, URL/File will show the link for opening it, Dynamic Resize input will show the values inside the grid, multilines and long strings will be shown inside popup window)
 * Allow changing aliases and configuration with tag management disabled. Tag management should mostly mean that new tags can't be added, or old ones removed
 * Tag detection improvement inside Word Content control

### Breaking change (.NET and JVM)

 * Navigation expression is now configured with two parameters. Default behavior for detecting end of expression has changed (previously it would end with a navigation character - DOT or specified navigation separator; now it will require parenthesis before). Previous behavior can be obtained by providing custom function for finding navigation metadata ending. Old method is available for backward compatibility, but the behavior is changed
 * (Java only) Configuration API renames to make it more Java standardized (IFormatter is now Formatter, etc..). Old signatures are available for backward compatibility with `@Deprecated` annotation, but will be removed in the future.

### Improvements and bugfixes (.NET and JVM)

 * (Java only) Java 17 support improvements. Reflection will now prefer public signatures when possible. Non public will still be selected if `typeVisibility(false)` is not specified
 * `UUID`/`Guid` will now be recognized as "primitive" type during schema analysis. This will prevent listing of its methods inside Templater Editor
 * Encode filename inside log during debugging. Only filename will be saved, without path
 * (.NET only) Fix saving images in debug log when image content was not a MemoryStream
 * Multi-range chart support. Previously Templater only supported a single range inside chart region definition. Now it will cope (in many cases) with multiple ranges
 * Detection of shape links to cells. Templater will now recognize them and ignore them, since they are populated based on the value of the linked cell. This will fix wrong context detection which was considering such tags for resize operations
 * SOLO license check. When license file was referenced, cope with BOM issues inside file which was preventing correct license check on v6 (specifying license information directly was working correctly) 
 * (Java only) XML sorting fix. Fix some internal sorting operations to be stable and thus avoid the: `Comparison method violates its general contract!`
 
## 2021-09-09 v6.1.0

### Templater Editor improvements

 * Save analysis result into XML. This can be used to programatically check if document was validated and how many issues were in the document
 * Embedded CSV management (allow importing/changing/removing embedded CSV files)
 * Save Templater Editor version into XML
 * Minor bugfixes

### Word improvements (.NET and JVM)

 * Support for AltChunk. This allows detection of tags in embedded documents (txt, html, xml, docx and docm). Embedded documents work transparently with context detection, so all features will continue to work as expected
 * `FileInfo` (.NET) and `File` (JVM) support. They will be added as AltChunk (embedded into the zip file). This allows easy HTML import or nesting multiple documents within a parent Word document
 * (.NET only) Embedded documents will work even on new .NET due to custom package management implementation. `System.IO.Packaging` on new .NET versions has various issues
 * Recognize Repeating Section Content Control as resizable region (in similar manner as sections). Previously Templater only allowed removal of content controls, but now it will also allow resize on Repeating Sections (Repeating Section Content Control does not exist on old Word versions)
 * Support for list-alike Content Controls (`DropDown` and `ComboBox`). They are recognized as resiable objects
 * Performance optimizations on tag removal (during `resize(tags, 0)` operation)

### Chart improvements (.NET and JVM)

 * Microsoft specific charts will now be recognized and managed (histogram, sunburst, ...)
 * Dynamic resize and `:horizontal-resize` support for chart management (this allows charts with dynamic number of columns/series)

### Excel improvements (.NET and JVM)
 
 * Improved Dynamic resize behavior inside table/named range. Templater will respect tag position and will adjust table/named range accordingly (both horizontally and vertically)
 * Cope with "non-standard" shapes in Excel. Templater will now work in cases when shapes are not saved in expected order
 * Bugfix with formula detection across different sheets. When referencing later sheet Templater would sometimes parse formula incorrectly
 * Improved ignoreErrors configuration behavior. Templater was importing this configuration into Excel files on "invalid" locations. Now they will be added in the expected location
 * Dynamic resize will respect `fixed` both vertically and horizontally. Previously it was respected only vertically. If previous behavior is required, `whole-column` can be used which will instruct Templater to still do push to the right before cloning region horizontally
 
### Other improvements and bugfixes (.NET and JVM)

 * Configuration for class visibility consideration. Currently Templater will scan (and try to use) public properties even on non public classes (when passed into processing or referenced through relationships). This raises warning on new Java versions due to planned restrictions in reflection behavior on future Java version. It also introduces performance overhead for Templater processing. For now the configuration defaults to allowing non public class usage, but this might be changed in the future
 * Bugfix for navigation plugin on dictionaries/maps. Previously only 1 metadata could be used, now they can be chained. This already worked as expected when processing objects
 * Allow blacklisting on primitive types (string, DateTime, number, etc...)
 * (JVM only) Java beans support on primitive types
 * Improved `:unprocessed` behavior during dictionary processing. When tags are shared, only tags from the same context will be removed. This will reduce problems when tags from different context were replaced
 
## 2021-08-03 v6.0.0

### Templater Editor improvements

 * Configuration options (custom regex, navigation, separator char,...)
 * Recently used file list (per product) for easier selection of file
 * Tooltips for issues are now displayed in multiline fashion
 * Allow double-clicking on tags for easier pasting - will position it to next cell when inside a table or spreadsheet
 * New suggestions and warnings (invalid collapse, tag sharing, collections across sheets, various others...)
 * Support for Length member on direct JSON import
 * Support for large JSON drag/drop. Such JSON will not be displayed in the preview text box, which supports up to 32k characters
 * Tags will be sorted alphabetically for easier navigation
 * When specifying password for entry, allow show/hide on password
 * Allow drag/drop JSON directly on tag management window
 * Various performance optimizations
 * Various other minor improvements and bugfixes

### Major breaking changes (.NET and JVM)

 * Minimal Java version is now Java 8. Previously it was Java 6. Scala 2.11 is no longer supported
 * Java version now supports try-with-resource pattern and thus replaces `flush()` method with `close()`. This brings it on the same level as .NET version
 * CancellationToken can now be passed into processing. This allows processing cancellation (e.g. in case of too long processing or resource constraints)
 * .NET `factory.Open` API now matches Java version with argument order (input stream, extension, output stream + additional new cancellation token)
 * Old `Open` methods are marked as obsolete and available for easier conversion purposes. They will be removed in next major version
 * Java replaces cancellation pattern from `Thread.interrupt()` to CancellationToken. There is a `forThread(Thread)` method on CancellationToken to replicate previous behavior. In case of cancellation `java.util.concurrent.CancellationException` will be thrown instead of previous `InterruptedException`
 * SOLO license from previous versions will not work on this version. Reporting and Enterprise license will continue to work without any changes

### Major spreadsheet performance and memory improvements (.NET and JVM)

 * Templater can now create much larger spreadsheet documents due to various memory optimizations
 * Processing complex spreadsheets is now significantly faster, due to major refactoring

### Bugfixes and improvements (.NET and JVM)

 * Spreadsheet configuration options. Templater can now restrict how many new sheets can be created during processing and supports ignoring warning for inconsistent formulas. This can be accessed through `ConfigureSpreadsheet()` options in configuration builder
 * Support for multiple tags in spreadsheet hyperlinks
 * List detection changes in presentations. Templater will now consider element as part of a list more often. This will result in list resize instead of whole slide resize
 * Context detection improvements. Some edge cases with tags not part of a collection, but resized with the whole document will not be replaced correctly
 * Preformance improvements with tag sharing
 * Memory and performance improvements when saving OOXML files
 * Improved schema type detection. List more tags not used as entries for reference processing
 * (.NET only) Support for IEnumerator type as streaming collection
 * Bugfix with Word table columns with when performing Dynamic Resize
 * (JVM only) Support for Optional types. Support for common java.time types
 * Bugfix for Spreadsheet chart cloning. When non standard (Microsoft specific) charts were duplicated, not all information was retained and on Java it could sometimes result in a corrupted document.
 * Early invalid XML detection. Previously Templater would save all characters into XML, unless they were "fixed" with low-level plugin. Now an exception will be thrown on such inputs.

## 2021-03-01 v5.2.0

### Templater Editor improvements

 * Run Templater action now supports custom entries
 * New entry type: Javascript with support for XMLHttpRequests
 * Entries can be saved along the document and password protected

### Digital signature (.NET and JVM)

 * Documents/presentations/spreadsheets can now be digitally signed via certificate and a private key
 * Builder API has new `Sign(certificate)` method which will apply digital signature at the end of processing
 * To avoid signature invalidation, Excel will be put into Manual formula recalculation mode, which will prevent formula updates

### Bugfixes and improvement (.NET and JVM)

 * Schema analysis improvements. More types will be analyzed even if they were not used in processing. This will resolve problem of some tags missing
 * Schema collection analysis improvements. Use first element of a collection for better tag detection
 * Better formula parsing exception. Templater does not support formulas which reference other documents, in which case parsing will fail. Now cell and sheet will be mentioned in the error message
 * Shared formula fixes. Templater often left shared formulas as-is, which caused problems on duplication, as the formulas were not modified in the expected manner on resize and horizontal resize. Now all shared formulas will be converted into regulard formulas and thus formula rewrite will behave as expected
 * Word tag binding improvement. If Word binding is used with tag in incorrect way, avoid Exception during processing, but rather just replace tag with the provided value

## 2020-12-21 v5.1.1

### Debugger integration bugfixes (.NET and JVM)

 * Recorded actions were not correct for embedded files (CSV and charts). Now only relevant actions will be listed
 * (JVM only) Schema and log information would not be saved correctly sometimes.

## 2020-12-16 v5.1.0

### Debugger integration with Templater Editor (.NET and JVM)

 * Templater can now log all actions which can be replayed in the Templater Editor. This can be used for learning, to better understanding what is going on, or for debugging purposes when some plugin is misbehaving.
 * Debugging can be enabled via `debugLog(capture)` in the `IEditorConfigurationBuilder`
 * Templater Editor can enable debugging directly from the UI before running Templater

### Low-Level API improvements and breaking changes (.NET and JVM)

 * Handler API now returns an enum instead of boolean flag. This allows for more fine grained behavior and some new use-cases out-of-the-box. Previous values are mapped as: false -> Nothing, true -> NestedTags
 * ThisTag return value can be used to indicate processing of only relevant tag, but let Templater process nested tags, which can be useful for resizing/collapsing.
 * WholeObject return value can be used to short circuit further processing of this object, through indicating that all tags from this object have been manually handled

### Bugfixes and improvement (.NET and JVM)

 * (.NET only) C# spreadsheet bugfix with table pushdown. Table would get corrupted in some scenarios when duplication and pushdown was applied to the same table.
 * Word table nesting improvements. Templater did not behave correctly when there was multiple nested tables on different rows which should be handled in a single resize operation.
 * Empty collection processing change. Try to process collection level tags for empty collections. This is not fully consistent as when collection is non-empty tag could be used for different purpose, but its expected behavior.
 * Excel sheet tag copying improvement. Improve handling of same tags on different sheets which were not shared. They should be initially setup the same way as if they were shared.
 * TableOfContent can now be removed. While Templater still takes special care of TOC Word field, it can now be removed when part of section which is removed.
 * Extra whitespace bugfix. Whitespace fix introduced in 2.9.3 was reverted as it was creating extra whitespace. Old Word versions do not display whitespaces as expected after unicode characters, while new Word versions behave correctly.

## 2020-10-27 v5.0.1

### Templater Editor integration bugfixes (.NET and JVM)

 * Keep name editor interface in JVM. Previously name was obfuscated.
 * During schema embeding in .NET for Word, read stream correctly to avoid exception during file loading

### Bugfixes and improvement (.NET and JVM)

 * Improved support for Content Controls. Nested content controls are supported now
 * Full row/column named range improvements. Templater will take special care of full row named range during pushdown
 * Performance improvements for formula duplication. In special cases with many cells, formulas will now be duplicated faster
 * Bookmark improvements. Respect out-of-order bookmarks

## 2020-10-22 v5.0.0

### Templater Editor improvements

 * PowerPoint Templater Editor is now available with support for common operations

### Templater Editor schema integration (.NET and JVM)

 * Builder API now allows configuring Templater Editor integration for schema building (this is not available in the SOLO version of the library)
 * When schema building is enabled, after processing finishes, template will remain as before, but additional metadata will be embedded in the document, such as listing of all available tags, for better and easier template management
 * Alias definitions can be specified through the builder API, which allows for more friendly tag usage, especially when combined with navigation customizations

### Navigation customization (.NET and JVM)

 * Templater now allows user defined expression during navigation. To enable this feature, navigation separator must be defined, e.g. ':'. Then built-in or user defined plugins can be invoked, e.g.: `[[items:at(0).name]]` will process only the first element of the collection instead of the entire collection. This vastly expands customization options, e.g.: custom sorting can be applied during navigation `[[object.collection:sort(property).description]]` where user defined plugin which will take care of `sort(property)` argument.
 * Navigation character can be changed. Default navigation character is . (DOT), but some else can be used, e.g.: / (SLASH)
 * Default TAG regex was changed to include new common characters: `:`, `(`, `)` and `|`

### Low-Level API improvements and breaking changes (.NET and JVM)

 * End of life for .NET 3.5 version
 * Resize now supports specifying exact tags which should be used. New API has been added for this purpose `Resize(TagPosition[], int)`. The old resize API uses various heuristics to process tags, while this new API does exactly what it was asked (when possible)
 * Handler API change. To support the new Resize API, handler accepts a new `int position` argument. Argument will be -1 for most cases, but when there is tag sharing involved, it will specify index of the shared tag
 * collapse-nested and collapse-to handlers only work in non-sharing mode (when position == -1)

### Bugfixes and improvement (.NET and JVM)

 * When replacing image via Alt Text, Templater will now always inject new image resource, instead of trying to replace it in some cases. This was causing problems where images were copy pasted around the document, which would point to same underlying resource
 * Missing tags processing improvements. In some cases when processing dictionary when nested tags were partially missing, Templater would not call `OnUnprocessed`
 * Table of Content Word improvements. Fix some regressions in range detection with TOC. Templater will now search for next entry or section/page break after TOC as border for duplication
 * License detection improvements. License watermark will now be different if specific common error happens (e.g.: old license from v2 is used)
 * Special object removal in Word. If `resize(tag, 0)` is called on a special object like an image, text box or WordArt (any other drawing object) Templater will remove only that object instead of looking for best context surounding that object
 * List resize bugfix. When resizing multilevel lists multiple times, Templater would sometimes incorrectly decide to resize region of the document, instead of only list
 
## 2020-08-24 v4.6.0

### Templater Editor alias integration

 * Templater Editor now allows alias definition in the Manage tags screen. Aliases allow for shorter tags, which helps in Word tables to prevent cell stretching. Aliases are matched against tag prefix

### Bugfixes and improvement (.NET and JVM)

 * Nested table/result set processing bugfix. When DataTable is used as a property in another DataTable and some tags are missing in the input tags would be processed incorrectly due to usage of :unprocessed on wrong tags. Now only relevant tags will be marked with :unprocessed metadata
 * OOXML processing will now recognized aliases defined via Templater Editor
 * Tags repeated in lists will not similarly to tags repeated in tables. If tags are only defined within a lists, only relevant lists will be resized instead of entire document
 * Dynamic resize improvements in PowerPoint. Previously only a single Dynamic resize was supported in familiar way within PP table. Now multiple such types can be used which will behave similarly to Word/Excel (eg jagged array for each row)
 * Table of Content Word improvement. Templater will now respect the page break configuration after TOC.
 
## 2020-07-02 v4.5.0

### Templater Editor for Microsoft Office 2007+ on Microsoft Windows

 * Templater Editor is Microsoft Office Add-In available to Reporting Team and Enterprise licenses. It provides improved user experience for managing Templater templates. Currently only available for Word and Excel
 * Tag management via schema import
 * Tag analysis with issue detection
 * Tag listing from defined schema
 * Running Templater processing from within Word/Excel UI for easy testing/REPL

### Existing image replacement (.NET and JVM)

 * Support replacing existing images when tag is added to their Alt Text. This finally allows the use of images in PowerPoint and expands the use cases since image can now be preconfigured in much more detail

### Improvements and bugfixes (.NET and JVM)

 * Word header/footer tag handling improvements. In some cases Templater would process tag only once, instead every time if tag was located in header/footer
 * Word 2016 chart coloring improvements. Charts created in new Word 2016 would reuse the same color for different points, instead of following the predefined coloring scheme
 * Word header/footer duplication improvements. For some documents with multiple headers/footers tags were replaced in incorrect order
 * Use System error console for logging unexpected problems with license check
 * Excel pushdown bugfix. In some cases Templater would push only a subset of cells down, instead of using wider range due to other elements (named ranges, etc..) present in the document
 * Detect PowerPoint lists in more cases. While custom bullet format was already detected in notes, now it will also be detected in slides
 
## 2020-05-26 v4.4.0

### Breaking change (.NET and JVM)

 * Low level replace API now accepts two additional arguments: tag (string) and metadata (string array). Existing configuration for that API needs to be adjusted on latest version.

### Improvements and bugfixes (.NET and JVM)

 * Support for CSV embedded within Excel. While such CSV could be processed by openning up xlsx zip, this way no special code changes are required for this scenario. Multiple CSV can be embedded. Tags can be repeated in worksheets and in CSV, although they might require special care
 * :unprocessed support in DataTable, IDataReader and ResultSet. Previously this data types would just leave tag as is. Now they will invoke OnUnprocessed handler on them
 * Renumbering of Word round shapes - previously Templater would not renumber this shapes which resulted in "corrupt" document
 * Performance improvements for Excel formula processing
 * PowerPoint fix for collection tag repeated on slides. Correctly sort tags on analysis for expected resize behavior
 * Word nested table/list renumbering. When resize affects more than a single table (nested tables) renumber lists in the process. This resolves problem of list numbering continuing across different tables
 
## 2020-02-04 v4.3.0

### Excel `resize(tags, 0)` improvements (.NET and JVM)

 * Templater will now hide rows or columns when they are fully affected by resize operation. Previously they would be left as-is
 * Sheets can now be "removed" with resize operation. Sheets will actually be set to very hidden, so they are not listed in the list of hidden sheets

### Excel bugfixes and other improvements (.NET and JVM)

 * Fix for whole row/column pattern duplication. If another sheet is referenced, after duplication formula will still be referencing other sheet (instead of loosing sheet reference)
 * Cope with formulas which reference table header using a tag. Previously this worked only if header is first processed and then formulas are duplicated. Now Templater will fix formulas at a later point if necessary. It is still recomended to first rename the header and then duplicate the formulas if possible. If possible it is recommended to avoid the use of tags in table headers when they are referenced by formulas.
 * Improve thread interrupt checking. Thread interrupt was missing on few critical places after resize operation.
 * Various performance improvements. Templater will now process sheets with merge cells much faster. Some allocations were removed
 * Basic support for array formula

## 2019-12-23 v4.2.0

### Scalable Vector Graphic (SVG) support (.NET and JVM)

 * Microsoft Office 2016 has native support for SVG. SVG can be passed as `XDocument`/`Document` data type.
 * SVG converter builder API. To optionally support older MS Office tools, SVG document can be converted into image which will be displayed on older Word/Excel versions.

### Excel range improvements (.NET and JVM)

 * Whole row/column patterns are now supported (eg: `A:B`, `5:5`, `$A:$A`, ...)

### Horizontal resize regression fix from v4.1.0 (.NET and JVM)

 * In some cases cell left of the resize range would be incorrectly moved to the right. Now cells left of the horizontal resize range will stay on the left.

## 2019-12-02 v4.1.0

### Image improvements (.NET)

 * .NET now has Templater specific ImageInfo type like Java. Since System.Drawing is not available in some environments (like Azure Functions) features can be replicated by using Templater specific ImageInfo directly. Previous System.Drawing.Image and Icon are still supported out-of-the-box via builtin plugins. Another managed only library can be used in such scenarios.

### XML improvements (JVM)

 * Stable XML setup. Previously Templater would use default factory instance for XML parser and transformer. If there were dependencies with `META-INF` services they could change the used parser. Templater will now check for new Java 11 API, if that does not exists will try to use Java version of Xalan (default XML parser) and if that is not available default to previous behavior of using default factory instance.
 * XML factory setup is now available via configuration instead of `System.getProperties`. Previous method of customizing XML is no longer supported.

### Method blacklisting (.NET and JVM)

 * Method/member blacklisting can be done via `blacklist` configuration API. This is useful to prevent access to some sensitive information.

### Bugfixes and improvements (.NET and JVM)

 * Dynamic Resize on lists and arrays will now check for **header** metadata. While previously only ResultSet/DataTable would rename columns in Excel table, this can now be done with lists/arrays as long as header metadata exists.
 * Dynamic Resize duplicate column name will now be localized only to that column. Previously if there was duplicate names in ResultSet all columns would be named via ColumnX pattern instead of provided names. Now unique names will remain, while only duplicate or empty ones will use ColumnX pattern.
 * Word header/footer duplication bugfix. In some documents header/footer naming would create file with already existing part. Templater will now check that header/footer file names are really unique.
 * Horizontal resize did not work is some cases when there was more than one column involved in the resize. Column styles were not copied correctly and formulas within the context could cause NPE.
 * Merge cell handling fixes on horizontal resize. Depending on the setup Templater could create "corrupted" document during stretching/copying of merge cells. Now it will handle them differently to avoid creating such problems.
 * Dynamic Resize on ResultSet/DataTable with only a single column did not rename column names previosly. Now Templater will set the column name even when there is only a single column and even no rows.
 * Fix formula references to named ranges in local sheet. Previously Templater assumed that named ranges have unique names when referenced in formulas. Now it will guard for cases when there are several named ranges with same name on different sheets.
 * Outline level (grouping) will now be respected during resize. On horizontal resize **whole-column** metadata was required to duplicate the entire column. Now this will be done also when there is outline level defined.
 * Shared formula will not be rewritten anymore on horizontal resize. Previously it was changed into a non-shared expression.
 * Thread interrupt check was missing in fast path Excel resize. Now thread interrupt will be respected even there, which resolves the issue of huge documents used on fast path.
 * Fix tag detection in documents with both TableOfContent and headers/footers. Previously Templater could throw XML exception on such documents.
 * Minor Excel performance optimizations and memory usage reductions.
 * Part naming improvements. Many embedded files will now follow stable pattern when created. Previously they were often using random names to remain unique.

### Breaking changes (.NET and JVM)

 * (JVM only) ImageInfo DPI renames. ImageInfo now matches .NET naming and data types.
 * `GetMetadata(tag, bool)` now returns only user defined metadata. Internal metadata is only available via `GetMetadata(tag, index)` API. This makes the API behavior consistent with the documentation.

## 2019-10-19 v4.0.1

### Image improvements (.NET)

 * Images will be saved in original format (when possible). Previously images were always saved as PNG.

### Bugfixes and improvements (.NET and JVM)

 * Cope with pivot tag. Previously if Pivot dimension was a tag, it would get lost after conversion.
 * Performance improvements with parsing formulas in Excel.

## 2019-09-11 v4.0.0

### PowerPoint support (.NET and JVM)

 * Templater now supports pptx and pptm files. Most regular features are available, such as: dynamic resize, slide duplication, table resizing, chart population, etc.

### License key change (.NET and JVM)

 * Templater v4 requires a new license key. License key from v1 will not work on v4 of the Templater.

### Bugfixes and improvements (.NET and JVM)

 * Support for inline string in Excel. Previously Templater did not support string values embedded directly into cell marked via special `inlineStr` attribute.
 * Various performance improvements.
 * Support for Java 11. Due to removal of several deprecated features in Java 11, license validation did not work as expected without [additional dependencies](https://stackoverflow.com/questions/52502189/java-11-package-javax-xml-bind-does-not-exist)
 * Avoid analyzing tags on resize which contain `:unprocessed` metadata (in Excel).
 * Fix merge cells handling/duplication in some edge cases in Excel.

## 2019-08-15 v3.2.1

### Bugfixes and improvements (.NET and JVM)

 * Detect built-in styles defined as lists. Previously Templater did not consider such parts of the documents as list and was treating them differently which resulted in different resize rules.
 * Cleanup all shared tags which are missing. Previously just the first tag was processed (marked with `:unprocessed` metadata)
 * Include missing tags in resize for dictionary collections when prefix is used. This fixes cleanup of missing tags which now goes over all (shared) of them
 * Support pivot multi-select filters. Templater will now ignore such tags instead of trying to process them.
 * Pushdown in Excel will cope with ranges which are up to maximum row number (1 million)
 * Include repeated tag which is not shared in Word resize. If tag is within the same context it should be included in the resize (even when repeated).
 * Avoid analyzing tags on resize which contain `:unprocessed` metadata (in Word).
 * Fix tag sharing in some edge cases in Word.
 * (.NET only) Cope with Excel document without count attribute on merge cells. Previously Templater would throw NPE
 * (JVM only) Add empty paragraph within table cell only when necessary (added in 3.0.0 but implemented incorrectly for JVM)

## 2019-06-17 v3.2.0

### Streaming improvements (.NET and JVM)

 * Allow specifying streaming size in `IDocumentFactoryBuilder`. Streaming size will be used as chunking size for DB reader types or `Iterable`/`Enumerable` types (non dictionary ones)
 * `Iterable`/`IEnumerable` types will be analyzed only once and processed in chunks (based on the streaming size). By default streaming size of 16384 is used. Previously Templater could consume part of input during analysis
 * Text flushing will use streaming size
 * Collection with known size will not be streamed (`ICollection` in .Net `Collection` in Java and `Seq` in Scala)

### Excel improvements (.NET and JVM)

 * Templater will now move drawing objects around on pushdown/pushright. This includes images, WordArt, Charts and similar objects which were previously not affected
 * Support for drawing objects: WordArt and TextBox
 * 3 letter columns support. Previously Templater only supported 2 letter columns (up to ZZ). Now columns up to ZZZ are supported.

### Android support (JVM)

 * Templater now works in Android (it requires global system parameter for ignoring unsupported XML features)
 * Internal image data structure is now public - ImageInfo
 * During setup Java image conversion should be disabled by turning off builtin low-level plugins (which do conversion of BufferedImage and ImageInputStream to ImageInfo)

### Bugfixes and improvements (.NET and JVM)

 * Chart data source will be adjusted on pushdown/other changes
 * Guard against sheet dimension over 1M rows - keep dimension at maximum size instead
 * Shared tags are now processed much faster - previously it was prefereable to use a new tag root instead of repeating same tag in a collection on multiple sheets
 * Performance optimization with large number of named ranges pushdowns (use more optimized algorithm)
 * (JVM only) Support adding image to an Excel document which didn't have images previously. This was already working in .NET version
 * Cope with Excel documents without dimension info. Non MS tools sometime create such documents
 * Cope with conditional formatting on pivots. Templater ignores such formatting, while previously it would fail during initial analysis
 * (JVM only) Support for Scala 2.13
 * Fix for text streaming on multi-row context. Previously Templater would corrupt the order and fail with OutOfMemory error when used in such scenario
 * substring metadata will not throw exceptions anymore. Instead empty string will be returned

## 2019-05-05 v3.1.0

### New configuration API (.NET and JVM)

 * Allow specifying format per each tag pattern through `withMatcher` on `IDocumentFactoryBuilder`

### Bugfixes (.NET and JVM)

 * Excel comments will now respect cell which they reference. Previously they were considered in same manner as header tags which is not correct
 * Stable tag sort during object processing. Tags should be processed in order of definition in the document (unless they require navigation). Since v2.9.4 they could be sorted in a way which causes problems due to use of handler plugins

### Dynamic type improvements (.NET)

 * Dynamic resize will now work on `IList<object>` as long as all elements are either `IList<object>` or `IList<string>`. This matches the behavior of JVM which due type erasure worked correctly for complex JSONs
 * OnUnprocessed will be called while processing dictionary even when it has no keys
 * Builtin collapse will now be invoked on empty maps even in JVM (previously it was invoked on empty Dictionaries in C# only)

## 2019-04-15 v3.0.0

### New tag format (.NET and JVM)

 * `[[tag]]`, `{{tag}}` and `<<tag>>` formats are now supported

### Unprocessed tags handling changes (.NET and JVM)

 * There is a new API in configuration builder for specifying custom behavior for handling unprocessed tags: `OnUnprocessed`
 * Previously unprocessed tags were handled differently for different processor types. Sometimes they were not handled at all which could lead to large memory usage due to bad context detection.
 * The default implementation will append :unprocessed on the tags which were expected to be handled, but input could not be matched (such as missing attributes in JSON, typos in document, etc...)
 * Few handlers still do not use such API (readers/data table/data set)

### Plugin customization APIs (.NET and JVM)

 * Now it's possible to disable builtin handlers/formatters during library initialization

### Java bean naming support (JVM)

 * Templater now recognizes bean naming and will match appropriate tag such as `name` with `getName` method.
 * Bean naming support can be disabled during library initialization
 * On naming conflict exact name will be used instead of bean name

### Bugfixes and improvements (.NET and JVM)

 * Exception is thrown if clone is called while CSV streaming has started
 * More interrupt checks on some places - Templater will now exit more quickly in case of thread interrupt
 * Cope with empty table cells in Word - when list is removed from a table it could have resulted in corrupted document due to missing paragraph. Templater will now inject empty paragraph so document is not corrupted
 * Cope with Word orientation change as last page - previously when a section is removed which changed the last orientation an empty last page would remain. Now document will be adjusted to look as expected
 * Support for tables without count in XLSX - LibreOffice sometimes removes some attributes which are not required. This fixes NPE which could happen in such cases
 * Improved resizing with headers in Word - previously Templater would not respect tags in headers as it should. This resulted in bad context detection and strange output document
 * Text sharing detection fix - sometimes sharing would be used on the wrong tag during CSV/text processing

## 2019-01-22 v2.9.6

### Bugfixes and improvements (.NET and JVM)

 * CSV streaming. Templater will now flush to stream and reuse memory after flushing when processing is done in a streaming way.
 * Word page removal improvements. Previously Templater required two sections around the tag to remove the content of the section. Now it will work from the start of the document to first section or from the last section to the end of the document too.
 * Java erasure improvements - Templater will now run resize(tags, 0) on empty collections as long as they are part of some object. Previously this worked as expected only for dictionaries, now it will work for most collections. .NET will match the behavior - which means that `List<object>` will now remove content of tags with a prefix.
 * CSV resize fix - processing would sometimes work correctly only when tags were in the "expected" order. Now processing should process tag as expected regardless of the order
 * Tags in Map skipping - regression from 2.7.5 fixed which incorrectly assumed that tags in map/dictionary keys will not have DOT (.). Before starting of nested processing, tags will also be matched with exact key names
 * CSV resize improvements - cope with removal of tags which are repeated inside context. Previously exceptions would be thrown in some cases during removal of such tags

## 2018-11-27 v2.9.5

### CSV (.NET and JVM)

 * Support for adding new tags within the document (previously new tags could have been added only at the end of the document)
 * Multiple row context handling improvements. CSV should now behave similarly to Excel in most scenarios
 * NullPointerException bugfix on ILowLevelReplacer in Java
 * Improved detection of invalid documents. Faulty documents which cause repeated resize of same tags will now be handled more gracefully instead of throwing NPE or element not found in dictionary errors

### Bugfixes and improvements (.NET and JVM)

 * Improved formula parsing support (improved support for single quotes and references to column names which start with #)
 * Better formula resizing. Formulas on other sheet than the one which is being resized will be adjusted (previously only formulas on the sheet being resized would get adjusted)
 * Cope with out of order tags within tables. In previous version partial solution to the problem was implemented. Now tags which are used within a table, but are not part of the row context will be processed in a special way to avoid leaving the tags unprocessed
 * Word hyperlinks will now be always escaped (to avoid producing invalid documents). Changed Java escaping algorithm to match .NET version

## 2018-10-09 v2.9.4

### Breaking changes (.NET and JVM)

 * `getMetadata(tag, all = true)` now excludes internal metadata. To see internal metadata one can pass all = false to see it for the first tag, or use the indexed based API which includes the internal metadata.
 * Tags in Excel headers/footers/sheet names are now ordered before the tags in cells

### Comments in spreadsheets (.NET and JVM)

 * Templater now recognizes, duplicates and moves comments in xlsx. Tags can be used in comments and will be considered as part of the context where the comment is pointing to.

### Bugfixes and improvements (.NET and JVM)

 * Performance improvement due to GetMetadata change. On large excel files which more than one nesting, GetMetadata(all) is called frequently added noticable overhead.
 * Sort tags during object processing in cases when there are tags of different level. This fixes an edge case where tag with lower level (eg `[[text]]`) was used in nesting, while tag with higher level (eg: `[[values.name]]`) was used before it. Templater would only process the nested tag once, and even :all metadata would not resolve this issue. Now tag will be processed as expected.
 * Excel resize with formulas performance improvements. When formulas reference other sheets, Templater will take a fast path on resize which will significantly speed-up the resize.
 * Hyperlink escaping. Hyperlinks in Word and Excel will now be URI escaped to avoid producing corrupted documents.
 * Support for data labels in Excel 2013. Templater will now handle graph data labels.
 * Multiple tags in embedded Excel. Same tag can now be used multiple times on Excel embedded within Word file.
 * Improved Word table context sharing. Support for scenario where same tag is used within and outside of table, and then it's resized.
 * Improved support for LibreOffice. Cope with LibreOffice specifics (not having section properties at the end of the document). This produces a different document where resize does not insert page break.
 * Charts sharing improvement. Support multiple charts even when they are defined within a table.

## 2018-08-28 v2.9.3

### Improvements (.NET and JVM)

 * Improved TableOfContent support in Word (TOC will not be duplicated on resize, only on clone)
 * NaN/Infinity will be converted into #NUM! error in Excel
 * JVM support for primitive nested arrays (eg double[][]) as source for Dynamic resize
 * Improved support for formulas in merged cells (they require special handling)
 * Support for utf8 extensions in .NET (as way to force UTF-8 encoding on the input). It was already supported in JVM
 * Support for other worksheet sources (so that Power Query can be used)
 * JVM will now rewrite XML file only if it's used by Templater (Power Query uses some files which were corrupted after being saved in Java).
 * `substring(start, count)` metadata will now behave as substring(start, min(max remaining characters, count)). This will stop throwing exceptions when count exceedes the string size
 * Text (CSV) processing will now work across multiple rows as a single context when appropriate
 * Improved support for queryable tables. Templater will now ignore tags in queryable tables (such as Power Query)

### Date/timestamp fixes/improvements (JVM)

 * Special support for java.sql.Date which will be converted into date (without the time part)
 * Improved support for java.util.Date which will now respect the timezone

### Security (JVM)

 * Enable XXE protection by default (to disable XXE protection set system property 'templater:xxe-protection' to 'false')
 
### Bugfixes (.NET and JVM)

 * Whitespace propagation - instead of just putting xml:space="preserve" on the xml element, now xml:space will be only put when the xml element has some non-whitespace character. Otherwise it will be appended to the first previous non-whitespace element and xml:space will be put on that element. This fixes some strange behaviors with spacing in some rare cases.
 * Nested named range detection - Templater will now consider all named ranges smaller than the outer ones as candidates (previously height had to be smaller)
 * Excel row cloning will now propagate or remove attributes during pushdown - this fixes the problem with row height/style not being correct after pushdown
 * When Excel formula is changed, all ranges will be afected (previously if formula had same range repeated multiple times, only the first one would get changed)
 * Use appropriate range on conditional formating during resize. This fixes the problem of not cloning conditional formatting
 * Merge cell stretching fix - respect existing named ranges during merge cell stretching
 * Better handling of customXml files (Templater would crash with NPE on some files)
 * .NET tag removal could result in array out of bounds exception

### Performance (.NET and JVM)

 * Major CSV changes so that CSV(TXT) files with million rows are possible (in seconds)
 * Optimized pushdown - when all cells bellow pushdown range are within the specified range a faster algorithm will be used
 * Various minor memory usage optimizations
 * Respect .NET forward only friendly XML implementation for various performance improvements
 
## 2018-07-01 v2.9.2

### Improvements (.NET and JVM)

 * Excel support for special escape codes. Templater will now recognize Excel _xNNNN_ codes and cope with them
 * Guard against bad resize in Excel by checking resize limit over 2M. Since Excel supports up to 1M rows in a sheet, throw Exception in such a case.
 * Add more thread interrupt checks during Excel resize
 * Cope with missing tags in DataReader/ResultSet. Previously on large documents with missing tags Templater would incorrectly resize from row 1:10000 on second resize if the missing tag started with the same prefix as an existing tag. This will not happen anymore since Templater will resize only the relevant tags
 * Improved context detection for tags in headers/footers. Templater will now work correctly with Watermarks even when used from maps/dictionaries without the need for :all metadata hack
 * Share context across Excel sheets even when there is only a single row in the collection
 * Fully rename tags in table headers. Header rename will now change the table definition

### URL bugfix (JVM)

 * Only convert URL/URI into a hyperlink when used in a nonspecial object

## 2018-06-14 v2.9.1

### Word 2016 charts (.NET and JVM)

 * When duplicating charts on a document created in Word 2016, Templater would create a document in a way that it wouldn't open on new Word versions. Cope with new Word requirements so that document opens on both new and old Word versions.

### Uri/URL type support in Word (.NET and JVM)

 * System.Uri and java.net.URL/java.net.URI are now recognized and replaced with hyperlinks in Word documents. Those hyperlinks are not fully fledged hyperlink feature, but rather just a field. Previously this could be done via inserting raw XML, but it would require also custom style if Hyperlink was not used in the document.

### Zip parsing (.NET)

 * After .NET 4.6 Microsoft Package API throws exception while parsing URLs valid in Word and Excel. Templater now copes with that issue by avoiding using Package API for relationships.

### Excel formula invalidation (.NET and JVM)

 * Templater will now always remove cached values from formula cells. Previously it was trying to do it on case by case basis. This fixes the problem of some formula values not being updated upon opening up the Excel

## 2018-05-26 v2.9.0

### Low level API change (.NET and JVM)

 * Replace with index now returns index of the next shared tag (instead of boolean). This allows for fast replacement of tags within the same context. Improve performance of resize when same tag exists across several tables/sheets.

### Excel bugfixes (.NET and JVM)

 * Stale formula values are now stripped. When Templater duplicates formula it will now remove cached values even for formulas which reference only table columns.
 * Sheet names with - are now properly escaped (quoted)
 * Fix stretching outer named range during pushing to the right. Previously they were stretched multiple times during resize.
 * During horizontal resize with rows which contain cells only to the right of the resize.
 * Share context during horizontal resize - this allows for repeating of same tag for horizontal duplication

### Deprecate build for .NET core 1.6 (.NET)

 * This version did not support images, data tables or thread interrupts. .NET core 2.0 supports full Templater feature set.

## 2018-05-05 v2.8.4

### Formula parsing fix (.NET and JVM)

 * When formula contained escaped quotes they were converted into a single quote

### .NET Standard v2.0 (.NET)

 * Templater can now be used as library from .NET core 2.0 or any other .NET Standard 2.0 compatible project. Unlike 1.6 version which is missing Image API, this version is fully compatible.

## 2018-04-29 v2.8.3

### Word vertical resize (.NET and JVM)

 * Similar to merge-nulls feature which does a hortizontal merging, a new span-nulls feature was introduced to Word which merges the cells vertically.

### Bugfixes and improvements (.NET and JVM)

 * Improved context detection for deep nesting. Change logic when section of the document is resized to work even for duplicated tags.
 * Don't try to resize a page if section is missing after the tags, but instead assume duplication up to the document ending
 * Correctly pick the section start when doing resize in JVM. Previously element before was taken
 * Fix a bug with multiple tables with same tags on different sheets. Exception was thrown in some cases.
 * Formula parsing fixes: better detection for quoting.
 * Shared formula fix: fix handling of shared fomula evalution.

## 2018-01-07 v2.8.2

### Thread interrupt improvements (.NET and JVM)

 * Check for thread interrupt on more places (during collection processing, in dynamic resize and before saving XML). This should cover most scenarios where increased memory usage would create problems for an instance.

### Resize limits (.NET and JVM)

 * If tags are configured in an "incorrect" way, Templater might decide to duplicate them in an unexpected way an in the process create memory problems. To avoid such problems, limit can be set on how many times a tag instance can be resized. In worst case it should be the depth of the hierarchy (by default it's 8). 

### Default regex change (.NET and JVM)

 * / will now be recognized by default in tags. During setup Templater can be configured with a custom regex since v2.7.0

## 2017-11-26 v2.8.1

### Excel improvements and fixes (.NET and JVM)

 * Cope with hidden named ranges. Excel uses hidden named ranges for filters on cells without table. Templater was incorrectly assuming a user defined that named range and was resizing range in an unexpected way.
 * When document was saved with a LibreOffice some numeric cells would be saved with extra metadata which Templater confused for a cached values. Now Templater will remove cached values only from formula cells.
 * Cell ordering regression bugfix. Templater could corrupt wide document by sorting cells in an incorrect order due to faulty optimization introduced in 2.7.x series.
 * Named range naming change. Instead of using guid in a named range name, now a temp_range_xxx pattern will be used for named ranges created with Templater

### Excel bugfixes (JVM)

 * Bugfix for map prefix matching introduced in 2.7.x series. When map had a values of 123 and 1234, Templater would sometimes only process the shorter version of the prefix.
 * Bugfix for named range cloning to another sheet. Previously Templater would not drop sheet related metadata (localSheetId)

### Thread interrupt aware (.NET and JVM)

 * Templater will now check during resize if a thread was interrupted/aborted and will throw appropriate exception for early exit. This is not supported on .NETStandard 1.6 due to missing thread API/exception.
 
## 2017-10-24 v2.8.0

### Word 2016 lists (.NET and JVM)

 * Word 2016 often saves lists in a new way which caused Templater not to consider that document part a list. Templater will now cope with such a list and renumber them accordingly

### Handler plugin breaking changes (.NET and JVM)

 * Previously invoking a handler would continue with replacing the values in the document. Now invoking a handler which returns true will stop further processing of the specific tag. To replicate old behavior more logic needs to be embedded into the handler
 * Collapse handler would invoke resize(tag, 0) on the assigned tag. This would often remove a lot more tags and would cause problems if this was done while processing a collection. Due to early exit and some additional checks, Templater should now cope with most of such documents (as long as other tags are nested)
 * New builtin handlers: collapse-nested (which will invoke resize 0 on assigned tag and all nested tags) and collapse-to(otherTag) (which will invoke resize(Array(tag, otherTag), 0)). If tags are repeated multiple times with resize 0 a different tags will be selected for removal

### Multiple shared tables within a sheet (.NET and JVM)

 * When a table is repeated multiple times and has same tags, previously during resize operation a minimum spanning context would be used to resize the region which would result in multiple tables. Now if all tags are within a table, instead of duplicating tables, new shared rows will be added to the existing tables instead. This is how Word works and matches the behavior of Excel if tables were in different sheets

### Performance improvements (.NET and JVM)

 * Spreadsheet formula which is stable (doesn't reference moving expressions) - for example reference to other columns whithin a table row will now require less processing during resize - which can result in large improvements
 * Various memory related improvements in JVM to reduce allocations.
 * Improved support for lists in large Word documents

### Other changes (.NET and JVM)

 * Cope with multiple references to custom XML. Previously Templater would throw an exception during analysis
 * Improved handling of Word documents missing/zero styles. Style 0 is a special style which Templater didn't handle previously
 * Formulas will be used as-is, instead of trimming them
 * Conditional formatting which references multiple regions will now be split into multiple conditional formattings which reference a single region. Previously Templater would fail to parse them properly
 * In .NET during resizing of an empty array within a dictionary invoke resize on all nested elements. This matches JVM and expected behavior
 * Ignore horizontal resize for empty collections. Currently instead of doing a pull to the left, Templater will replace all relevant tags with an empty value. Previously it would break with an exception
 * In JVM change the way context info is read in Excel. Previously it would remain the same as at the time when tag was created. Now it can change over time
 * Invoking removal of tags (resize 0) will now remove duplicate tags within a context in Excel. This is useful since Excel often don't have a defined region such as a table or named range, so region is defined based on the minimum spanning range matching all the tags (without duplicates). In Word the behavior is the same since tags for duplication are often put within a table or a list
 * When graph is used within a Word Office saves some of the cached values as strings (due them being defined as tags). When opening document within a Word those string values are converted to numbers (if possible) and graph is adjusted accordingly. Templater will now behave the same - it will convert string cache to number cache if all values are numbers.
 
## 2017-09-07 v2.7.5

### Concurrency regression (JVM)

 * In 2.7.4 an incorrect optimization was introduced which did not work correctly for concurrent processing. While new ITemplater instance is provided from factory.open, processors and formatters are reused

### List numbering regression (.NET and JVM)

 * Reverted change to list numbering done in 2.7.0 and make a better fix for list nesting within table for JVM.
 
## 2017-09-04 v2.7.4

### Named ranges (.NET and JVM)

 * Fixed bug in JVM with incorrect usage of nested and intersecting ranges.
 * For multiline rows, sometimes named ranges would be cloned by incorrect offset.
 * Take into account outer named ranges for cloned contexts. This fixes the resizing of named ranges in various scenarios

### Other changes (.NET and JVM)

 * Add support for Excel graphs in Word with multiple row templates. Previously only the last row was properly updated.
 * Share row in Word even for collections with one element. This fixes the problem when second collection was left unprocessed when it had only one element.
 * Cope with partial inputs during dictionary/map processing. Often when converting JSON into dictionaries, some leafs are omitted or stopped early with a null. Templater will now often process those elements (when they are in collection) as if the values were null. This should fix the problem when sometimes it looked like Templater was caught in a infinite loop.
 * Take merge cells into account when doing resizing. If merge cells is at the end of the context, context should be widened to accommodate for it.
 * Various JVM memory allocation improvements

## 2017-07-31 v2.7.3

### Excel bugfix (.NET and JVM)

 * Bugfix for corrupting wide spreadsheets at the column Z introduced in 2.7.2

## 2017-07-31 v2.7.2

### Performance improvements (.NET and JVM)

 * Fixed noticeable regression introduced for large documents which contain multiple same tags within the context
 * Various minor and major performance improvements for large xlsx documents

### Formula rewriting improvement (.NET and JVM)

 * Formula rewriting will work as expected in more scenarios, such as rolling sums

## 2017-07-04 v2.7.1

### Excel image bugfix (.NET and JVM)

 * In some Excels an exception can be thrown from Templater when a drawing reference contains relative path. Now Templater will cope with relative path to drawing

## 2017-07-01 v2.7.0

### .NET Standard v1.6 (.NET)

 * Templater can now be used as library from .NET core 1.0 or any other .NET Standard 1.6 compatible project (if you experience problems on .NET core during call to Configure method on Configuration class, try using Configure method on Builder instead).

### Maven release (JVM)

 * Since v2.6.0 Templater is available on Maven central.
 
### XML bindings (.NET and JVM)
 
 * XML binding handling has been improved and Templater will now support proper replacing, resizing and cloning with XML bindings (for limited number of scenarios).

### Improved context detection (.NET and JVM)
 
 * Context detection in Word has been changed to support same collection multiple times by default. Previously *repeat* metadata had to be used (or a different property with same collection) before Templater would process collection at multiple places.
 * Removed *repeat* metadata and introduced *no-repeat* to invoke old behavior.
 * Repeat by default has also been introduced in Excel. This simplifies scenarios when same collection is used on different sheets.

### Other fixes and improvements (.NET and JVM)
 
 * .NET bugfix for updating embeded chart inside Word when only a single row is used.
 * Improved handling of tags across sheets. When a collection is resized Templater will now better handle scenarios where a same collection is shared across multiple sheets and when same tags are used across those sheets.
 * JVM version will now cope better with various Word documents due to more robust handling of XML elements.
 * TAG customization. During setup Templater can be configured to accept a different TAG regex (base element). By default Templater used [-+@\w\s.,!?]+ as base regex element.
 * Bugfix for JVM list inside table resize. In some scenarios Templater would not copy list nested inside table as expected.
 * NullPointerException bugfix in JVM in some list resizing.

## 2017-04-08 v2.6.0

### Support for tags in sheet names (JVM and .NET)

 * Templater will now detect {{tag}} inside sheet name. Tag can't have metadata (since : is not an allowed char in sheet name). If invalid name is provided Templater will throw an exception. If tag is detected inside sheet name, sheet context will be used for resize.

### Improved image support (JVM)

 * Templater will now detect DPI for BMP type correctly.

### Excel fixes (JVM)

 * When chart data source was resized this was not reflected in actual chart. Now Templater will reflect changes on chart and thus include new data in filters.
 
### Other fixes/changes (JVM and .NET)

 * Column min/max fixes. Templater will now guard against setting too large value for column max. During push to the right, sometimes min/max would get invalid values which would result in Excel complaining during startup. This is now fixed.
 * Pivot tables are no longer renamed. PivotTable can have name unique within a sheet. Since Templater only duplicates pivot tables on full sheet resize, there is no need to actually rename the table.
 * In some cases clone of multiple sheets with multiple tables would result in wrongly referenced table. This is fixed now.

## 2017-03-18 v2.5.2

### DPI changes (JVM)

 * Templater will default to 96dpi since this is the default on Windows. Previously it used 72dpi which resulted in somewhat larger pictures in documents (33% larger due to 96/72 = 1.33). Default DPI can be controled through System properties as 'templater:dpi' setting.
 * Support for ImageInputStream which uses the actual DPI from the image and will save it in the actual format. When Image/BufferedImage is used DPI is not preserved and image is saved as PNG. To preserve the image format and DPI ImageInputStream (often directly loaded from File) needs to be used.
 
### DataSet relation fix (.NET)

 * previously Templater supported only simple master-detail relationship which worked in subset of cases. Now Templater will cope with multi-level relationships and resizing correctly in more cases.

### Wide columns guard (.NET and JVM)

 * Templater doesn't supports columns with three letters. In some cases push to right can stretch some stuff over the maximum supported size. Templater will cope better with some of such scenarios now.
 
## 2017-01-27 v2.5.1

### Section support in Word (.NET and JVM)

 * sections are now considered resizable elements (section above and bellow the tag will make a resizable context). This allows more complex resizing with the use of continuous breaks (sections).

## 2017-01-09 v2.5.0

### Replace plugin (.NET and JVM)

 * Templater now supports plugin on the Replace(tag) methods. This allows more customization scenarios and handling of problematic input. Word/Excel don't support some XML characters and while Templater will replace chars such as (char)31 into XML representation, this will still result in "corrupted" document. Now such characters can be stripped from the input or an exception can be thrown.

### XML metadata (.NET and JVM)

 * By default when XML is passed to Templater it will append it after the detected tag. Sometimes this results in whitespace garbage, so to avoid this two new keywords are introduced: remove-old-xml, replace-xml and merge-xml.
 * remove-old-xml will delete the XML tree path where the tag was detected. This is useful for removing old whitespace
 * replace-xml will replace children of XML tree with the one provided. This can be used for setting colors, or some other attributes and values.
 * merge-xml will add missing attributes and elements to the XML tree where the tag was detected. This can be used to set the color attributes, while not removing other parts in the tree (such as other tags)
 
### Horizontal resize improvements (.NET and JVM)

 * Previously horizontal resize only worked on single column width. Now it supports multi-column setups. If will also copy merge cells within the detected range.

### Improved content control support (.NET and JVM)

 * Templater will now remove stdContent cache if it contains a tag and will replace the actual XML file instead the cached values. This adds support for content controls which show document properties.

### Bugfixes (.NET and JVM)

 * Dynamic resize would throw exception if element with a height of one was passed in, but it had to do a pushdown. Now it skips the pushdown as expected
 * XML tree detection. In case when element was not a direct child of the body, Templater would throw an exception that if was unable to detect it

## 2016-09-10 v2.4.2

### Merge cell bugfix (.NET and JVM)

 * In some scenarios (when resizing collection inside other collection, while merge cell was in the outer collection) merge cells did not stretch correctly. This resulted in "corruption" of the document.
 
### List renumbering bugifx (.NET and JVM)
 
 * In version 2.4.0 lists renumbering was activated too often. This resulted in all list items having the number 1. Templater will now correctly renumber the list as before and will support renumbering in few new scenarios (list inside table). If list inside table doesn't contain any tags, it won't be renumbered.

## 2016-08-10 v2.4.1

### Nested collection resizing bugfix (.NET and JVM)

 * If nested collection contained multiple collections with only a single item, Templater would not replace items correctly.
 
### Performance improvements (.NET and JVM)

 * Several minor performance improvements (nested collections, large documents).

## 2016-07-26 v2.4.0

### Diagram bugfix (.NET and JVM)

 * If document contains diagrams, resulting document will not get "corrupted"

### New features (.NET and JVM)

 * Footnotes and endnotes - Templater now recognizes and replaces footnotes and endnotes. Their context will be definition of the note which allows fore multiple replacement of tags within the context and on the note.
 * merge-nulls internal metadata - Dynamic resize and normal replacement/resizing will now check for merge-nulls metadata and create/stretch a merge cell inside Excel/Word table
 * dynamic resize style copying - Dynamic resize will now clone style of the starting cell. This can be avoided by using fixed metadata which will instruct Templater to use existing cells, instead of pushing them down and cloning styles.
 
### Performance improvements (.NET and JVM)
 
 * various performance improvements for large documents.

## 2016-06-11 v2.3.3-1

### Excel bugfix (JVM)

 * During resizing tags with metadata Templater would overwrite first tag in first context during resize operation. Now it will respect user metadata.

## 2016-06-04 v2.3.3

### Excel features (.NET and JVM)

 * XML support. Feature parity between Word and Excel for importing raw XML into document. This allows for custom coloring, bolding and other rich text features. Can be easily used to corrupt document.
 * Conditional formatting support. Templater will now recognize and handle conditional formatting feature. Resize will duplicate/stretch or move it around the document. This is useful for defining custom colors for cells (eg. green for positive, red for negative values)

## 2016-05-26 v2.3.2

### Word tag detection improvements (.NET and JVM)

 * Detect tags defined for shapes, watermarks and similar objects.

### Map collection processing bugfix (JVM)

 * During analysis of tags using input for map collection Templater could miss some of the keys. Now all keys will be correctly processed.

## 2016-04-28 v2.3.1

### Word context detection improvements (.NET and JVM)

 * Improved context detection for shared collections. Previously Templater often had to use workaround with duplicate properties with same name to correctly handle repeating tables. It will now work in more cases correctly. Added an example to show off such use cases.

### Merge cell cloning/resizing fix (.NET and JVM)

 * During resizing, if there is an outer context, merge cells which were outside of the tag context, but within the outer context were stretched instead of copied. Now merge cells will be stretched only if they have greater or equal height to the inner tag context.

## 2016-04-26 v2.3.0

### API improvements (.NET and JVM)

 * Low level API now has getMetadata(tag, index) and replace(tag, index, value) methods. This allows support for more complex scenarios and easier document reflection.

### Improvements (.NET and JVM)

 * XML in Word will now better support import scenarios. Array of paragraphs, tables and other elements can be sent for import. If table is imported, it should have expected style in target document. Otherwise it will look style-less.
 * Due to new low level API, some scenarios are now better support. An example of such scenario is when a collection is shared among two different tables. Previously only first table would be correctly filled. Now tables will be filled correctly in more scenarios. An example of such feature is available in examples.
 * Hyperlink support in Excel. Previously links only worked in Word. Now they work also in Excel. An example showing usage of links in Excel is available in examples.
 
### Hypelink detection fix (JVM)

 * Hyperlinks were not detected correctly if they were part of an expression. For example `[[email]]` tag worked inside hyperlink, but `mailto:[[address]]` did not. Now both scenarios will work correctly.

## 2016-03-11 v2.2.1

### Excel fixes (.NET and JVM)

 * after resize AutoFilter will now be readjusted to include new size.
 * resizing simple case (fast path) will correctly clone columns when there are missing columns inside range. Previously fast path assumed all columns are used, which was incorrect and would result in cloned cell being copied into previous column.

## 2016-02-12 v2.2.0

### Charts in Word - embedded Excel (.NET and JVM)

 * Excels can be embedded within Word. This is the basis for displaying charts inside Word. Embedded Excels are now analyzed and processed as any other tags.

### Excel improvements (.NET and JVM)

 * Named range below pushdown range was used during analysis to change pushdown range to more appropriate one. Now merge cells will also be used for that purpose. This improves the generic pushdown behavior since it can often use document as is, without introducing named range just for Templater.
 * Merge cells are now stretched only if they are not in any of the resize tags
 * Push to the right - dynamic resize will now move cells right of the context to the appropriate place
 * horizontal resize - instead of vertical resize, context can be duplicated to the right with the use of :horizontal-resize metadata. Best fitting range will be used for resize, but with the help of :whole-column metadata whole column can be resized instead.
 * Formula rewriting - on resize, pushdown and push to the right formulas are updated to reflect changes done with the document.

### Calc chain removal (.NET and JVM)

 * Calc chain is no longer updated and is removed from xlsx file. It's optional and keeping it up to date requires much more logic due to formula rewriting feature.

### Word improvements (.NET and JVM)

 * Search for common table when tags are detected inside table. This improves resize logic for nested tables where some tags are in outer and other in inner table.
 * Simple support for Simple Word fields. This allows usage of such features in other libraries (such as QR code)

### Collection type probing (.NET)

 * For non-sealed classes collection will be probed for its elements. This improves type detection in collection, which uses base type detected in collection. Previously Templater used type declaration for generic types to assume content of the collection. It will still fallback to that logic for empty collections.

### Newline splitting in Word bugfix (JVM)

 * String with newlines must be carefully added into Word. JVM version used incorrect order during such conversion.

### Map processor improvements (.NET and JVM)

 * null values for keys are not ignored, but rather processed as null values. This fixes the issue of collapsing for that specific key. This is only change for JVM. .NET already behaved in such a way.
 * keys in dictionary which were processed are still probed for exact tag match. This allows hide to work on such keys.

### Collapse fix (JVM)

 * zero length string now triggers collapse

## 2015-09-01 v2.1.5

### Map/Dictionary null value fix (.NET and JVM)

 * If null value was used inside Map/Dictionary Templater replaced the specific tag only once. Fixed logic for null values, so it behaves as for any other value. Same tag will be replaced multiple time when expected.

### Word dynamic resize improvements (.NET and JVM)

 * During dynamic resize all values were converted to string. If complex data type, such as Image was used, Templater didn't inject picture into the document, but rather only displayed a string value. Now Templater will respect special data types as long as it's not nested (dynamic resize type, eg. Object[][] will behave as string).

### Custom XML TransformerFactory (JVM)

 * Specific xml transformer factory can be specified with 'templater:TransformerFactory' system property. This is useful in scenarios when Java has embedded an old/custom version of Xalan and produces invalid documents (such as IBM Java 6).

### Excel image position bugfix  (.NET and JVM)

 * Excel images were displayed one row bellow the tag. Now they will be displayed at the row where the tag was defined.

## 2015-07-29 v2.1.4

### Replace all logic improvements (.NET and JVM)

 * For certain data types replace all is the default behavior (such as maps/dictionaries). This only worked correctly when applied on root path. Templater will now carry along this information which will result in better replace all default logic. This should result in less usage of :all metadata workaround.

### Pushdown on dynamic resize (.NET and JVM)

 * Pushdown didn't work correctly if DataTable/arrays were passed to Templater low level API. It started pushing below the new size of the collection instead of the location of the tag.
 
## 2015-07-08 v2.1.3

### Excel row height cloning (.NET and JVM)

 * If row has a custom height, it should always be copied. Previously Templater only copied height when there was no rows below template row. Now it should copy it on each resize.

### Context detection fixes in Excel (.NET and JVM)

 * Few inconsistencies between libraries have been resolved. JVM will now match behavior of .NET in few cases when they differed (incorrect logic for context detection on pushdown)
 * Table will now be included in calculation during context detection. If a table stretches out of detected context (based on provided tags) context will be adjusted to new maximum size.

### Excel dimension update (.NET and JVM)

 * If dimension attribute exists in the sheet, it will be updated (at lest the height part).

## 2015-06-16 v2.1.2

### Whitespace fixes (.NET and JVM)

 * Templater recognized empty space at the start or end of tag value and injected "preserve" attribute into Word. Not it will recognize all whitespace characters, not just empty space. Also, if multiline string was passed, Templater did not behave correctly. Now it will behave correctly even when multiline string is passed (which is converted to string parts with line breaks)

### Context detection changes (.NET and JVM)

 * Templater high level API injects special context information into tag so it can recognize which tags belong together. Now it will insert such metadata at the beginning instead of the end of tag metadata. This helps with some context detection scenarios.
 
## 2015-05-21 v2.1.1

### Page break change (.NET and JVM)

 * Previously page break was inserted after each resize of the whole document in Word. Since section properties are cloned on each resize of the whole document this caused "two duplicate" page break instructions. Now page break will be added only if one of the found tags contains 'page-break' metadata. Cloning doesn't insert page break anymore. This allows for user to specify behavior of next page by defining section break style which will be used.
 
### `IEnumerable<XElement>`/`Element[]` support in Word (.NET and JVM)

 * To ease the use of XML type usage, collections will now be recognized (IEnumerable in .NET and array in JVM). This simplifies HTML -> DOCX conversion since often multiple paragraphs need to be inserted after a single tag.

### Empty spreadsheet bugfix (.NET and JVM)

 * Templater will now work as expected when spreadsheet only has data in headers. Previously it crashed.

### :all metadata on DataTable/DataSet/ResultSet (.NET and JVM)

 * :all metadata is used to force Templater into replacing found tag everywhere in the document. This is useful when different sections contain same tag, but Templater is unable to conclude that it should replace specific tag. This feature will now work on other types as expected.
 
## 2015-03-26 v2.1.0

### Resizing document with picture fix (.NET and JVM)

 * If picture was used within context which was resized, Templater didn't changed id to unique value. This caused document to be reported as corrupted.

### Prefix matching fix (.NET and JVM)

 * In dynamic data types (DataRow/DataTable/Dictionary) prefix was sometimes matched incorrectly. If two prefixes started with same values due to order of keys/columns Templater would ignore the second prefix if it started with first prefix. This is somewhat improved, but it can still happen if "." is used inside names. This fixes the problem where key "10" was ignored in dictionary since key "1" already existed.

### Macro enabled documents support (.NET and JVM)

 * docm and xlsm are now recognized and processed

### Hyperlink support in Word (.NET and JVM)

 * hyperlinks (url, email) and their tooltips will now be processed by Templater. When defining hyperlink tag Word will replace []/{} with escaped values, but that is expected.

### System.Xml.Linq.XElement/org.w3c.Element support in Word (.NET and JVM)

 * If XML element type is detected content will be injected "as is" after matching node name is found. Existing tag is replaced with empty string and node is matched starting from the existing location. Be careful with this feature since document can easily be corrupted.

## 2015-03-03 v2.0.4

### Multiple rows resizing fix (.NET and JVM)

 * First context did not behave correctly if tag was used in multiple lines. Now Templater will replace same tag in entire first context.

## 2015-02-10 v2.0.3

### Word regression fix (.NET)

 * Templater didn't filter out non headers/footers when cloning header/footer document part. Filter added back.

### Empty formatter change (JVM)

 * Empty formatter will now work on empty strings as .NET version does.

## 2015-02-02 v2.0.2

### Excel bugfixes (.NET and JVM)

 * Formula rename fix. Support more scenarios for copying formula to new sheet.
 * When named ranges references table Templater would try to extract sheet name from such formula (but it doesn't exists). Now they will be ignored instead of trying to process it.
 * All copied named ranges are now global. Templater was assigning local sheet id to copied named ranges, now it will copy even local ranges to global (all ranges have unique random name).
 * Named range nesting change. Nested ranges which are inside currently processed range were always copied. Now they will be copied only if their height is smaller than the outer one. This allows for formulas on named ranges which should be resized instead of copied.

## 2015-01-28 v2.0.1

### Word clone/resize header/footer fix (.NET and JVM)

 * On some documents header/footer info is contained inside document, not at the end. Templater didn't correctly copied/renamed info in that case.
 
## 2015-01-26 v2.0.0

### New tag format (.NET and JVM)

 * `[[tag]]` and `{{tag}}` formats are now supported
 
### User registered plugins (.NET and JVM)

 * While Templater is heavily based on plugins, it allowed only for Enterprise users to use such feature. Now all users can benefit from plugin registration which should enable them to support all kind of customizations while using latest version of Templater.

### Improved type support (.NET and JVM)

 * Templater supports IDictionary/Map (with all keys as string type), but now it works in even more scenarios. Collection of dictionary/map is now supported as first level argument. Non string type keys are supported (if they are all strings) which means more types are recognized, such as Hashtable (used in PowerShell).
 * Previous .NET version supported non-generic IDictionary only, now `IDictionary<string, object>` is supported too
 * If Object[] is detected, Templater previously assumed Object type and couldn't do much with it. Now it will look into the collection for the actual type and will use that instead. Note that if a collection is empty Templater will not be able to resize the collection to 0 since it will not know which properties it needs to match.
 * Low level API support for jagged lists (`List<List<String>>` and `IList<IList<string>>` in .NET and `List<List<?>>` in Java and List[List[AnyRef]] in Scala). Templater supported jagged arrays so you could use String[][] for dynamic resize. To better support deserialized JSON support for list is added. Note that Json.NET will deserialize into its own JToken/JArray so manual conversion will be required.

### Some/None support (Scala)

 * Content of Some is extracted so actual type is used.

### Simple spreadsheet formula clone support (.NET and JVM)

 * In Excel if another table or named range is referenced in a cell formula, during cloning Templater will rewrite that formula to 
use newly created table/named range. Only simple names are supported. This currently doesn't work in resize, but only in clone.

### Minor spreadsheet fixes (.NET and JVM)

 * Chart c:tx detection added.
 * Support for non-trivial sheet name. When formula is created in chart, fix reference to sheet which name is escaped.

### Resource license embedding (JVM)

 * Templater will also check project resources using current Thread class loader for license file. If default factory() or build() methods are used, Templater will run in unlicensed mode when license is not found. If license is not found when exact path is specified, an exception will be thrown

### ResultSet/DataSet bugfixes (.NET and JVM)

 * Every 1000th row was skipped. This is fixed in new version. Algorithm still uses resize in 1000 chunks, so performance is unaffected.
 * Tabled used by result set would end up with an empty row at the end. Templater is now more careful about resizing and will not resize it more than necessary.

### Word picture bugfix (.NET and JVM)

 * Unique picture numbering. Currently pictures embedded in header/footer and copied over to next page retained same picture id. Now they will always get a new unique id.

## 2014-11-04 v1.9.9

### Internal memory stream fix (.NET)

 * .NET uses Seek to increase stream size. Fixed internal stream to cope with it.

## 2014-10-27 v1.9.8

### Spreadsheet context detection improvements (.NET and JVM)

 * Major refactoring of spreadsheet context detection to support more scenarios without the use of named ranges and tables. Multiple groupings are now supported and properly detected.
 * Breaking change on resize behavior in spreadsheets. While previously it was enough to specify a single tag inside a named range, now Templater will behave differently whether all tags in named range are specified or only a subset of them are. If all tags are specified, named range will be used as a context, otherwise, best context spanning all specified tags will be used.

### Null image handling bugfix (JVM)

 * Null value for BufferedImage property will no longer throw NullReferenceException

## 2014-10-25 v1.9.7

### Spreadsheet fast path bugfix (.NET and JVM)

 * Currently only one row is supported when fast path is activated. Templater did not check for row count which resulted in invalid row duplication.

## 2014-09-27 v1.9.6

### Spreadsheet nested context regression fix (.NET and JVM)

 * It seems that a while ago nested contexts in spreadsheet stopped working correctly in some scenarios. Fixed bugs which caused it to stop working.
 
### Performance and memory usage improvements (.NET and JVM)

 * Various minor improvements which should reduce GC garbage on large documents. Prefer arrays over other data types, reuse string names when possible, few local caches and plain loops instead of functional expressions.

### Context replacement in collections (.NET and JVM)

 * While context was detected using base item type in collection (this only affects collections with different element types), processing of such collection recalculated properties for each item. Reuse properties used for context detection instead.

### Spreadsheet performance improvement (.NET and JVM)

 * Added fast path when resizing a single-row range (or table). If range is last on sheet it can use faster algorithm to resize itself

### XML parameter for deferred node processing (JVM)

 * DOM supports deferred and eager node processing. Default is deferred, but it's better to use eager for small documents. Parameter can be controlled with global system properties key: 'templater:defer-node-expansion', for example System.getProperties.setProperty("templater:defer-node-expansion", "true")

## 2014-09-16 v1.9.5

### Major spreadsheet performance optimizations (.NET and JVM)

 * When text is found beneath range/table which will be resized Templater will push it down. Pushdown algorithm optimized.
 * When table/range is resized, cell styles needs to be copied to all new rows. Copy algorithm optimized.

### Major document performance optimizations (.NET and JVM)

 * Table resize improvements. Critical part of resize algorithm improvements from O(n2) to O(nlogn). This can drastically improve large document generation (from 5 min -> 5 sec)

### Spreadsheet performance optimizations (.NET and JVM)

 * Faster internal cell comparison results in less memory usage and faster reports

### Memory leak fix (.NET 3.5)

 * Optimized loading of XML in .NET which fixes memory leak from usage of large strings (in large documents)

## 2014-09-02 v1.9.4

### Document picture id fix (.NET and JVM)

 * OOXML format specifies that picture id inside document must be unique. While Word 2007 will not complain, Word 2010 will refuse to load such document. During page copying, Templater will copy pictures and change ids for newly copied pictures

## 2014-08-17 v1.9.3

### Custom XML parser improvements (JVM)

 * Templater by default loads default XML parser. In some environments custom XML parser is injected as default parser which doesn't support all of the features required by Templater. Custom parser used by Templater can be specified with 'templater:DocumentBuilderFactory' system properties key. If invalid parser is detected an exception explaining how to handle this scenario is thrown

### Spreadsheet extLst element fix (.NET and JVM)

 * If image was inserted into document and custom extensions were defined in the document, Templater would corrupt xlsx document since it didn't respect correct ordering for elements.

### Spreadsheet size/performance improvements (.NET and JVM)

 * Cell content will be lazily copied instead of eagerly. This usually results in much smaller documents (if large number of static cells are copied)
 * Cell interaction improved. Optimize usage of strings when interacting with cell values

### Document clone changes (.NET and JVM)

 * If :all metadata is used along with :clone metadata, Templater will now respect it. Previously cloning document disabled replacement of multiple same tags. This is useful when same tag is used in different contexts, such as two different tables. Templater can be forced to replace both tags by specifying :all metadata.

### Document header/footer fix (.NET)

 * Header/footer relation was not copied. This was a problem if header/footer contained a picture or some reference which caused relation to be created alongside it

### Spreadsheet formula recalculation (.NET and JVM)

 * Spreadsheets with saved formula values will not be recalculated by default. If option specifying should they be calculated is missing, turn it on so that every formula is recalculated when document is opened. This feature can be disabled by specifying fullCalcOnLoad attribute in calcPr element as 0.

### Tags recognition changes and optimizations (.NET and JVM)

 * Tags now support ascii characters, numbers and few special characters: - + . , ! ?
 * Global cache for short tags (<128 chars) to improve parsing speed

### Property detection fix (.NET)

 * Ignore indexed properties. Since they require arguments, they can't be processed

## 2014-05-30 v1.9.2

### Document spacing fix (.NET and JVM)

 * Since space is truncated in Word by default, add preserve space attribute to appropriate XML elements

## 2014-03-13 v1.9.1

### Spreadsheet whole row detection (.NET and JVM)

 * Support for $X:$X format for whole row selection. Converted internally to $A$X:$B$Y format

### Visibility fix (JVM)

 * Skip non-public modifiers in JVM

### ResultSet/DataTable expansion fix (.NET and JVM)

 * Can't replace table header values, without replacing table column names. Skip replacing headers if table is not extended

### Bool metadata parsing fix (JVM)

 * Parse bool with ',' and '/' but not ',/'

### Proguard visibility fix (JVM)

 * Changed visibility of proguarded classes so they don't leak into IDE autocomplete feature

### Merge cell copy fix (.NET and JVM)

 * When range is selected as cell size, merge cells were not copied.

## 2014-02-23 v1.9.0

### API change: input/output stream (.NET)

 * Support input/output stream in .NET as exists for JVM. This is standard way for interaction in web apps.

### Document performance improvements (.NET and JVM)

 * Use faster XML element comparison to reduce memory usage and improve speed.

### Deadlock issue fixed (.NET 3.5)

 * Added lock around global type properties cache. Since .NET 3.5 doesn't have support for concurrent cache, add explicit lock to avoid dictionary lock issue

### Specialized internal memory stream (.NET)

 * Since .NET suffers from LOH issues, added special memory stream which is used during internal processing

## 2013-11-16 v1.8.2

### Java BigDecimal support (JVM)

 * Only Scala BigDecimal was supported. doubleValue or alternative methods had to be used to work around this.

### Performance improvements (JVM)

 * Switch to Java collections whenever appropriate. It seems Scala has large performance issues.
 * Date conversion optimization in spreadsheet. Since Excel has special date value, changed conversion algorithm to O(1) operation

### Concurrency issue fix (.NET 4.0)

 * Switched to concurrent dictionary in .NET 4.0

### Collapse metadata fix (.NET and JVM)

 * It seems that collapse needs special path to be able to work as expected. Moved collapse handler from formatters to special handlers group

## 2013-09-05 v1.8.1

### Improved tag detection in documents (.NET and JVM)

 * Tags are usually spread over several text runs. Improve detection for correct start and end positions in them. Algorithm is still slightly based on heuristics, but it's much better now.

## 2013-06-07 v1.8.0

### Zero arguments method fix (.NET and JVM)

 * Only methods with zero arguments are allowed to be picked up by Templater during scanning.
 * Added check in JVM to ignore methods which return void type

### Performance optimizations (.NET and JVM)

 * Cache properties picked up with reflection in global cache.

### Nested table fix (.NET and JVM)

 * Only topmost tables must be copied. When tables are embedded in tables, Templater must be careful when it should copy each table.

### Named range fix (.NET and JVM)

 * All named ranges must be copied, not just the first one found in context. This fixes strange behavior on spreadsheet without table in a context.

### Hierarchical dictionary support (JVM)

 * Improved Map processor to be able to support more complex scenarios. This is used on demo page on Templater website.

## 2013-05-28 v1.7.6

### Context detection improvements (.NET and JVM)

 * Context metadata is added to cloned pages. This allows for collection to use resize and replace on same context for pages.
 * Renamed context identifier from _contextInfo to _ci

## 2013-05-17 v1.7.5

### Resizing improvements in documents (.NET and JVM)

 * If all tags are inside single table (even in list inside a table) resize that table. While this fails if table exists inside list, it will work for most use cases.

### List numbering fix (.NET and JVM)

 * Copy with missing numbering attributes (which LibreOffice knows to omit).

## 2013-04-18 v1.7.4

### Dynamic resize improvements (.NET and JVM)

 * Allow dynamic resize without table (only with range).
 * Fixed issue with tables without headers.

### Image size fix in spreadsheets (JVM)

 * Better image size detection. Use floating math for improved precision. JVM still assumes 72dpi for images.

## 2013-02-19 v1.7.3

### Image size fix in documents (.NET and JVM)

 * Better image size detection. Use floating math for improved precision. JVM still assumes 72dpi for images.
 * Rounding number issue fixed in .NET.

### Object context detection (.NET and JVM)

 * When object processor starts he should try to replace all tags if prefix is empty. This fixes the problem with multiple tags on the same page and removes requirement for :all metadata (in most scenarios).

### Formula conversion fix (JVM)

 * Added conversion of `[[equals]]` to = (formula) in spreadsheets. This is an ugly hack to support formulas in spreadsheets, since current tag format is disallowed by Excel. Ideally Templater should support alternative tag format, such as {{tag}} with doesn't have issues in Excel.

### Clone flush fix (JVM)

 * Clones were not flushed. This made using them kind of useless.

## 2012-12-13 v1.7.2

### Clone fix (.NET)

 * Last XML element was not copied during cloning.

## 2012-11-30 v1.7.1

### Document header bugfix (JVM)

 * Wrong name was used for header.

### Collapse formatter (.NET and JVM)

 * Support collapsing of document regions on empty collections or null values. This allows for conditionals which hide part of the document with :collapse metadata.

### Word list resize fix (.NET and JVM)

 * List should resize event if doesn't have style defined (Word 2010 creates lists without style).

## 2012-05-28 v1.7.0

### Dynamic resizing (.NET and JVM)

 * Recognize special data types such as DataTable, two dimensional/jagged arrays and handle them in a special way. Instead of just replacing predefined template with values in one dimension (down), resize detected object (table) in both dimensions. If :headers metadata is used, inject headers from DataTable too.

### Clone fix (.NET and JVM)

 * Fix numbers and names for copied objects (bookmarks and shapes). Since they must be unique, give them unique names.

### Context improvements (.NET and JVM)

 * Resize tables or lists only when they contain all of the specified tags. Otherwise fall back to page/sheet/document instead.
 * Support multi-row contexts in table/range. Context doesn't need to be single row anymore, since it will be tracked more precisely. All provided tags used in resize will decide context range.

### Formatter improvements (JVM)

 * Detect java.util.Date and redirect :format metadata arguments to SimpleDateFormat. Now :format(yyyy-MM-dd) will output expected result
 * Ignore String type for formatting

### Coping with malformed documents (.NET and JVM)

 * Excel doesn't remove named range from deleted sheet. Check if sheet exists during named range scanning.

### Metadata for fixed tables (.NET and JVM)

 * :table or :fixed metadata can be used to instruct Templater not to resize specified table. This is useful when table has predefined set of rows, with all rows filled with tags. Templater should not resize such tables, but it should remove/clear out missing rows (it will replace unmatched tags with empty string)

### Merge cell fix (.NET and JVM)

 * Merge cell needs to be stretched when they end after range which is resized.

### Performance optimizations (.NET and JVM)

 * Use better data structure in processors for improved performance.
 * Changed various Scala data types to Java data types to improve performance. Scala is really slow otherwise.