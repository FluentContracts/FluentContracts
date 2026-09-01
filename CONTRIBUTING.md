# Contribution Guidelines

As a community, we want to help each other, provide constructive feedback, and make a better product. Of course, our [code of conduct](CODE_OF_CONDUCT.md) must be followed at any time.

## Issues

### Before creating an issue

Evaluate whether your topic is going to be a valid issue:

- Is your issue more of a question? Ask on [GitHub discussions](https://github.com/FluentContracts/FluentContracts/discussions))!
- Have you checked existing/closed issues? Is your version behind?
- Have you read the relevant [changelog notes](https://github.com/FluentContracts/FluentContracts/blob/master/CHANGELOG.md)?

### When creating an issue

Choose one of the [issue templates](https://github.com/FluentContracts/FluentContracts/issues/new/choose) and fill it out as well as possible. This includes, but is not limited to:

- State the issue as short as possible (more likely there's time to comprehend it)
- Use [markdown](https://docs.github.com/en/get-started/writing-on-github) for code, logs, and other special text fragments
- Don't paste images when they're showing log output or exception messages
- [Refrain from making demands or expressing disappointment](https://mikemcquaid.com/2018/03/19/open-source-maintainers-owe-you-nothing) 

### After creating an issue

Once the `triage` label is removed from your issue, you will know how it is seen from the project's perspective:

>If your issue is labeled as `good first issue`, consider sending a pull-request

The issue will be addressed sooner or later depending on the priority, available time, and your commitment to the project. In rare cases, it might also be closed due to missing resources.

## Pull-Requests

### Before creating a pull-request

In your own interest of getting a pull-request merged (timely):

- Open an [issue](https://github.com/FluentContracts/FluentContracts/issues/new/choose) first — every
  pull-request should reference one (`Closes #123`), so the reasoning behind a change is findable
  without reading its diff
- Make sure your employer allows contributions
- Branch your work off from the `master` branch
- Get familiar with the coding conventions

### When working on a pull-request

- Aim for qualitative and readable code
- Follow the coding style of the existing codebase
- Make sure the project builds, and all tests pass
- Add tests! We are aiming at high test coverage. PRs without added tests for bugfixes or new functionality will not be accepted

### When creating a pull-request

- [Link the issue it relates to](https://docs.github.com/en/issues/tracking-your-work-with-issues/linking-a-pull-request-to-an-issue) (unless it's trivial)
- Check all the applicable boxes

### After creating a pull-request

- Don't bother to rebase your pull-request if commits have been force-pushed
- [Don't "push" your pull-request](https://www.igvita.com/2011/12/19/dont-push-your-pull-requests/)

## Releases

`master` is the only long-lived branch, and **every merge into it is released**: the
`release` workflow tests, packs, pushes the package to NuGet and creates the matching
GitHub release and tag. Publishing uses
[Trusted Publishing](https://learn.microsoft.com/en-us/nuget/nuget-org/trusted-publishing),
so there is no NuGet API key stored in the repository: the workflow exchanges a GitHub
OIDC token for a key that lasts an hour.

The version is worked out by GitVersion from the most recent tag, and the patch is
incremented by default. To release a minor or major version instead, include a directive
in the merge commit message:

```
+semver: minor
+semver: major
```

> Take care when writing about these directives in a **commit message**: GitVersion reads
> them out of commit messages, so quoting one there will actually request that bump.

To merge something that does not belong in a package — a change to the guides, the CI configuration,
the issue templates — put the `skip-release` label on the pull-request. The merge is still built and
tested, but nothing is published and the version does not move.

Use the label rather than the pull-request title. The merged subject does not always come from the
title — with a single commit on the branch, GitHub's squash default uses that commit's subject instead
— so a marker in the title can simply never reach the commit the release reads. A label is read off
the pull-request itself, so the commit count cannot affect it.

Note that `docs/PackageReadme.md` and `assets/icon.png` ship *inside* the package even though they look
like documentation, so changes to those do need a release to reach anyone.

Release notes are generated automatically from the merged pull-request titles, so give
your pull-request a title that reads well in a changelog. `CHANGELOG.md` is kept as a
curated summary of notable changes on top of that: add your entry under `## [Unreleased]`,
and the release workflow renames that section to the published version for you and commits
the result back to `master`.